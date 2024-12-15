namespace Framework.Mediator.DomainEvents;

public interface IDomainEventPublisher
{
    Task<Result<Empty>> Publish<TEvent>(TEvent @event, CancellationToken cancellationToken)
        where TEvent : IDomainEvent;
}