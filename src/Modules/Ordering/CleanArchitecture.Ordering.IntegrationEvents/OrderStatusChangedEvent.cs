using Framework.Mediator.IntegrationEvents;

namespace CleanArchitecture.Ordering.IntegrationEvents;

public class OrderStatusChangedEvent : IIntegrationEvent
{
    public const string EventTopic = "OrderStatusChangedEvent";
    public static string Topic => EventTopic;

    public required int OrderId { get; init; }
    public required OrderStatus OrderStatus { get; init; }
}
