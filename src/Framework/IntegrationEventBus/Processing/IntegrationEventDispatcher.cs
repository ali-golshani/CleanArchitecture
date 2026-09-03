using System.Reflection;
using System.Runtime.ExceptionServices;
using IntegrationEventBus.Internal;
using IntegrationEventBus.Topology;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationEventBus.Processing;

internal sealed class IntegrationEventDispatcher(
    IntegrationEventTopology topology,
    IIntegrationEventSerializer serializer,
    IServiceScopeFactory scopeFactory)
    : IIntegrationEventDispatcher
{
    public async ValueTask DispatchAsync(
        StoredEventDelivery delivery,
        SubscriptionDefinition subscription,
        CancellationToken cancellationToken)
    {
        var eventDefinition = topology.GetEvent(delivery.EventName);

        if (!subscription.Routes.TryGetValue(eventDefinition.EventType, out var handlerType))
        {
            throw new InvalidOperationException(
                $"Subscription '{subscription.Name}' does not subscribe to event '{delivery.EventName}'.");
        }

        if (handlerType is null)
        {
            throw new InvalidOperationException(
                $"Subscription '{subscription.Name}' has no handler for event '{delivery.EventName}' " +
                "in this processor process.");
        }

        var payload = serializer.Deserialize(delivery.PayloadJson, eventDefinition.EventType);
        var context = new IntegrationEventContext
        {
            EventId = delivery.EventId,
            EventName = delivery.EventName,
            Topic = delivery.Topic,
            SubscriptionName = subscription.Name,
            OccurredAtUtc = delivery.OccurredAtUtc,
            Attempt = delivery.Attempt,
            CorrelationId = delivery.CorrelationId,
            CausationId = delivery.CausationId
        };

        await using var scope = scopeFactory.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);
        var handlerInterface = typeof(IIntegrationEventHandler<>).MakeGenericType(eventDefinition.EventType);
        var method = handlerInterface.GetMethod(nameof(IIntegrationEventHandler<object>.HandleAsync))
            ?? throw new InvalidOperationException(
                $"Handler type '{handlerType.FullName}' does not expose HandleAsync.");

        try
        {
            var invocation = method.Invoke(handler, [payload, context, cancellationToken]);
            if (invocation is not ValueTask task)
            {
                throw new InvalidOperationException(
                    $"Handler type '{handlerType.FullName}' returned an unexpected result.");
            }

            await task.ConfigureAwait(false);
        }
        catch (TargetInvocationException exception) when (exception.InnerException is not null)
        {
            ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
            throw;
        }
    }
}
