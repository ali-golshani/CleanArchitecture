namespace IntegrationEventBus.Internal;

/// <summary>
/// Storage-neutral representation of an event delivery selected for execution.
/// </summary>
internal sealed record StoredEventDelivery(
    long DeliveryId,
    long EventSequence,
    Guid EventId,
    string EventName,
    string Topic,
    string PayloadJson,
    DateTimeOffset OccurredAtUtc,
    string? CorrelationId,
    Guid? CausationId,
    int Attempt,
    DateTimeOffset? FirstFailedAtUtc);
