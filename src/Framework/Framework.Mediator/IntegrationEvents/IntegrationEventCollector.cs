namespace Framework.Mediator.IntegrationEvents;

internal sealed class IntegrationEventCollector(ICorrelationIdAccessor correlationIdAccessor) : IIntegrationEventCollector
{
    private readonly Queue<IIntegrationEventEnvelope> events = [];

    public void Add<TEvent>(TEvent @event) where TEvent : IIntegrationEvent
    {
        var correlationId = correlationIdAccessor.CorrelationId
            ?? throw new InvalidOperationException("CorrelationId is not initialized for the current request execution.");

        events.Enqueue(new IntegrationEventEnvelope<TEvent>
        {
            MessageId = Guid.NewGuid(),
            CorrelationId = correlationId,
            Payload = @event,
        });
    }

    public IReadOnlyCollection<IIntegrationEventEnvelope> Drain()
    {
        var collectedEvents = events.ToArray();
        events.Clear();
        return collectedEvents;
    }
}
