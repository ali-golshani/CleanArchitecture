namespace Framework.Mediator.IntegrationEvents;

public sealed class IntegrationEventEnvelope<TEvent> : IIntegrationEventEnvelope
    where TEvent : IIntegrationEvent
{
    public required Guid MessageId { get; init; }
    public required Guid CorrelationId { get; init; }
    public required TEvent Payload { get; init; }

    public string Topic => TEvent.Topic;
}
