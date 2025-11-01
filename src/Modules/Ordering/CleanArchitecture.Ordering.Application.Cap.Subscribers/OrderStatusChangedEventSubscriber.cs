using Framework.Mediator.Extensions;
using CleanArchitecture.Ordering.Commands;
using DotNetCore.CAP;
using CleanArchitecture.Ordering.IntegrationEvents;

namespace CleanArchitecture.Ordering.Application.Cap.Subscribers;

public sealed class OrderStatusChangedEventSubscriber(ICommandService commandService) :
    SubscriberBase(commandService),
    ICapSubscribe
{
    [CapSubscribe(OrderStatusChangedEvent.EventTopic, Group = "Group-A")]
    public Task Handle_A(OrderStatusChangedEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{GetType().Name} A: Order-Id = {@event.OrderId}");

        var command = new Commands.Example.Command
        {
            Id = @event.OrderId,
        }
        .WithCorrelationId(@event.CorrelationId);

        return Handle(command, cancellationToken);
    }

    [CapSubscribe(OrderStatusChangedEvent.EventTopic, Group = "Group-B")]
    public Task Handle_B(OrderStatusChangedEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"{GetType().Name} B: Order-Id = {@event.OrderId}");

        var command = new Commands.Example.Command
        {
            Id = @event.OrderId,
        }
        .WithCorrelationId(@event.CorrelationId);

        return Handle(command, cancellationToken);
    }
}
