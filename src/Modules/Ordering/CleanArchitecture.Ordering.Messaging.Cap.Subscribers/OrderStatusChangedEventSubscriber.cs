using Framework.Mediator.Extensions;
using CleanArchitecture.Ordering.Commands;
using DotNetCore.CAP;
using CleanArchitecture.Ordering.IntegrationEvents;
using Framework.Mediator;

namespace CleanArchitecture.Ordering.Messaging.Cap.Subscribers;

public sealed class OrderStatusChangedEventSubscriber(ICommandService commandService) :
    SubscriberBase(commandService),
    ICapSubscribe
{
    [CapSubscribe(OrderStatusChangedEvent.EventTopic, Group = "Group-A")]
    public Task Handle_A(OrderStatusChangedEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{GetType().Name} A: Order-Id = {@event.OrderId}");

        var command = new Commands.DoNothings.Command
        {
            Id = @event.OrderId,
        };

        return Handle(command, cancellationToken, new RequestExecutionOptions
        {
            CorrelationId = @event.Header.CorrelationId,
        });
    }

    [CapSubscribe(OrderStatusChangedEvent.EventTopic, Group = "Group-B")]
    public Task Handle_B(OrderStatusChangedEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{GetType().Name} B: Order-Id = {@event.OrderId}");

        var command = new Commands.DoNothings.Command
        {
            Id = @event.OrderId,
        };

        return Handle(command, cancellationToken, new RequestExecutionOptions
        {
            CorrelationId = @event.Header.CorrelationId,
        });
    }
}
