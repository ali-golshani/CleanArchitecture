namespace CleanArchitecture.Mediator;

public interface IDomainEventHandler<in TEvent>
    where TEvent : IDomainEvent
{
    Task<Result<Empty>> Handle(TEvent @event);
}
