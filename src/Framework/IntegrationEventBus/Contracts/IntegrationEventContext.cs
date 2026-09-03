namespace IntegrationEventBus;

/// <summary>
/// Immutable metadata associated with a handler invocation.
/// </summary>
public sealed record IntegrationEventContext
{
    /// <summary>
    /// Gets the stable identifier shared by all deliveries of this event. It can be used as an
    /// idempotency key when calling an external system.
    /// </summary>
    public required Guid EventId { get; init; }

    /// <summary>
    /// Gets the stable, configured event contract name.
    /// </summary>
    public required string EventName { get; init; }

    /// <summary>
    /// Gets the topic that owns the event's ordering boundary.
    /// </summary>
    public required string Topic { get; init; }

    /// <summary>
    /// Gets the subscription currently processing the event.
    /// </summary>
    public required string SubscriptionName { get; init; }

    /// <summary>
    /// Gets the UTC time at which the business event occurred.
    /// </summary>
    public required DateTimeOffset OccurredAtUtc { get; init; }

    /// <summary>
    /// Gets the one-based number of the current delivery attempt.
    /// </summary>
    public required int Attempt { get; init; }

    /// <summary>
    /// Gets the optional identifier used to correlate related events and operations.
    /// </summary>
    public string? CorrelationId { get; init; }

    /// <summary>
    /// Gets the identifier of the event that caused this event, when available.
    /// </summary>
    public Guid? CausationId { get; init; }

}
