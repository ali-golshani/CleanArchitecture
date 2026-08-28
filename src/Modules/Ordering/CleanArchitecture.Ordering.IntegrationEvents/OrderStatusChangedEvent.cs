using Framework.Mediator.IntegrationEvents;

namespace CleanArchitecture.Ordering.IntegrationEvents;

public class OrderStatusChangedEvent : IIntegrationEvent
{
    public const string EventTopic = "OrderStatusChangedEvent";
    public string Topic => EventTopic;

    public required IntegrationEventHeader Header { get; init; }
    public required int OrderId { get; init; }
    public required OrderStatus OrderStatus { get; init; }
}
