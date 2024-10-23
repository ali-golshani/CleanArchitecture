using CleanArchitecture.Mediator;

namespace CleanArchitecture.Ordering.Commands.IntegrationEvents;

public class OrderStatusChangedEvent : IIntegrationEvent
{
    public bool FireAndForget => false;

    public required Guid EventId { get; init; }
    public required DateTime EventTime { get; init; }
    public required Guid? CommandCorrelationId { get; init; }
    public required int OrderId { get; init; }
    public required OrderStatus OrderStatus { get; init; }
}
