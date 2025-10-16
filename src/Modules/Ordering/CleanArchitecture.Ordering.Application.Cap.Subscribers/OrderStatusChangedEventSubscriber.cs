using Framework.Mediator.Extensions;
using CleanArchitecture.Ordering.Commands;
using DotNetCore.CAP;
using CleanArchitecture.Ordering.DomainEvents;

namespace CleanArchitecture.Ordering.Application.Cap.Subscribers;

public class OrderStatusChangedEventSubscriber(ICommandService commandService) :
    SubscriberBase(commandService),
    ICapSubscribe
{
    [CapSubscribe(OrderStatusChangedEvent.EventTopic)]
    public Task Handle(OrderStatusChangedEvent @event, CancellationToken cancellationToken)
    {
        var command = new Commands.Example.Command
        {
            Id = @event.OrderId,
        }
        .WithCorrelationId(@event.CorrelationId);

        return Handle(command, cancellationToken);
    }
}
