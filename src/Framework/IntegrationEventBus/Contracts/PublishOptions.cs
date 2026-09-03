namespace IntegrationEventBus;

/// <summary>
/// Optional metadata supplied when publishing an integration event.
/// </summary>
public sealed record PublishOptions
{
    /// <summary>
    /// Gets an optional caller-assigned event identifier. A new identifier is generated when this
    /// value is not supplied.
    /// </summary>
    public Guid? EventId { get; init; }

    /// <summary>
    /// Gets the UTC time at which the business event occurred. The publisher's current UTC time is
    /// used when this value is not supplied.
    /// </summary>
    public DateTimeOffset? OccurredAtUtc { get; init; }

    /// <summary>
    /// Gets an optional identifier used to correlate related events and operations.
    /// </summary>
    public string? CorrelationId { get; init; }

    /// <summary>
    /// Gets the identifier of the integration event that caused this event, when applicable.
    /// </summary>
    public Guid? CausationId { get; init; }

}
