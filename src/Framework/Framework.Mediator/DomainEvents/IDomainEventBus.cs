namespace Framework.Mediator.DomainEvents;

public interface IDomainEventBus
{
    ValueTask Post(IDomainEvent @event);
    IReadOnlyCollection<IDomainEvent> Events { get; }
}
