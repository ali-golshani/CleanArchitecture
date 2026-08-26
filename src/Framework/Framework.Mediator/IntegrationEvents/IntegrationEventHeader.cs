namespace Framework.Mediator.IntegrationEvents;

public sealed class IntegrationEventHeader
{
    public required Guid CorrelationId { get; init; }
}
