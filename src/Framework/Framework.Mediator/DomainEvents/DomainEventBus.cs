namespace Framework.Mediator.DomainEvents;

public class DomainEventBus : IDomainEventBus
{
    private readonly Queue<IDomainEvent> events = [];

    public IReadOnlyCollection<IDomainEvent> Events => [.. events];

    public ValueTask Post(IDomainEvent @event)
    {
        events.Enqueue(@event);
        return ValueTask.CompletedTask;
    }
}
