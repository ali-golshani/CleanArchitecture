using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.IntegrationEvents;
using Framework.Mediator;
using IntegrationEventBus.Abstractions;

namespace CleanArchitecture.Ordering.Messaging.SqlEventBus.Subscribers;

public sealed class OrderStatusChangedEventSubscriberA(ICommandService commandService) :
    SubscriberBase(commandService),
    IIntegrationEventHandler<OrderStatusChangedEvent>
{
    public ValueTask HandleAsync(
        OrderStatusChangedEvent integrationEvent,
        IntegrationEventContext context,
        CancellationToken cancellationToken)
    {
        Console.WriteLine($"{GetType().Name} A: Order-Id = {integrationEvent.OrderId}");

        var command = new Commands.DoNothings.Command
        {
            Id = integrationEvent.OrderId,
        };

        return new ValueTask(Handle(command, cancellationToken, new RequestExecutionOptions
        {
            CorrelationId = integrationEvent.Header.CorrelationId,
        }));
    }
}

public sealed class OrderStatusChangedEventSubscriberB(ICommandService commandService) :
    SubscriberBase(commandService),
    IIntegrationEventHandler<OrderStatusChangedEvent>
{
    public ValueTask HandleAsync(
        OrderStatusChangedEvent integrationEvent,
        IntegrationEventContext context,
        CancellationToken cancellationToken)
    {
        Console.WriteLine($"{GetType().Name} B: Order-Id = {integrationEvent.OrderId}");

        var command = new Commands.DoNothings.Command
        {
            Id = integrationEvent.OrderId,
        };

        return new ValueTask(Handle(command, cancellationToken, new RequestExecutionOptions
        {
            CorrelationId = integrationEvent.Header.CorrelationId,
        }));
    }
}
