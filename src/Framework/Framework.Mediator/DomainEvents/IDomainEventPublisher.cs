namespace Framework.Mediator.DomainEvents;

public interface IDomainEventPublisher
{
    Task<Result<Empty>> Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
}