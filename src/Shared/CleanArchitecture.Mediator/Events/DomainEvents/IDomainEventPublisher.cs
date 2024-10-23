namespace CleanArchitecture.Mediator;

public interface IDomainEventPublisher
{
    Task<Result<Empty>> Publish<TEvent>(TEvent @event) where TEvent : IDomainEvent;
}