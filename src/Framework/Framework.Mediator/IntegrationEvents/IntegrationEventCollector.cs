namespace Framework.Mediator.IntegrationEvents;

internal sealed class IntegrationEventCollector(ICorrelationIdAccessor correlationIdAccessor) : IIntegrationEventCollector
{
    private readonly Queue<IIntegrationEvent> events = [];

    public void Add<TEvent>(Func<IntegrationEventHeader, TEvent> eventFactory)
        where TEvent : IIntegrationEvent
    {
        var correlationId =
            correlationIdAccessor.CorrelationId
            ?? throw new InvalidOperationException("CorrelationId is not initialized for the current request execution.");

        var @event = eventFactory(new IntegrationEventHeader
        {
            CorrelationId = correlationId,
        });

        events.Enqueue(@event);
    }

    public IReadOnlyCollection<IIntegrationEvent> Drain()
    {
        var collectedEvents = events.ToArray();
        events.Clear();
        return collectedEvents;
    }
}
