namespace Framework.Mediator.IntegrationEvents;

public class IntegrationEventBus : IIntegrationEventBus
{
    private readonly Queue<IIntegrationEvent> events = new Queue<IIntegrationEvent>();

    public IReadOnlyCollection<IIntegrationEvent> Events => events.ToArray();

    public ValueTask Post(IIntegrationEvent @event)
    {
        events.Enqueue(@event);
        return ValueTask.CompletedTask;
    }
}
