using Framework.Mediator.IntegrationEvents;

namespace CleanArchitecture.Ordering.Commands.IntegrationEvents;

public class OrderStatusChangedEvent : IIntegrationEvent
{
    public const string EventTopic = nameof(OrderStatusChangedEvent);
    public string Topic { get; } = EventTopic;

    public bool FireAndForget { get; } = false;

    public required Guid? CorrelationId { get; init; }
    public required int OrderId { get; init; }
    public required OrderStatus OrderStatus { get; init; }
}
