namespace Framework.Mediator.DomainEvents;

public interface IDomainEventHandler<in TEvent>
    where TEvent : IDomainEvent
{
    Task<Result<Empty>> Handle(TEvent @event, CancellationToken cancellationToken);
}
