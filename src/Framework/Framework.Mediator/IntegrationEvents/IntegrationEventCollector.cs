namespace Framework.Mediator.IntegrationEvents;

internal sealed class IntegrationEventCollector : IIntegrationEventCollector
{
    private readonly Queue<IIntegrationEvent> events = [];

    public void Add(IIntegrationEvent @event)
    {
        events.Enqueue(@event);
    }

    public IReadOnlyCollection<IIntegrationEvent> Drain()
    {
        var collectedEvents = events.ToArray();
        events.Clear();
        return collectedEvents;
    }
}
