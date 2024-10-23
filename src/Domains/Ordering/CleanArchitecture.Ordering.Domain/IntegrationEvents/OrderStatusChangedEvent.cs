namespace CleanArchitecture.Ordering.Domain.IntegrationEvents;

public class OrderStatusChangedEvent : IntegrationEvent
{
    private OrderStatusChangedEvent() { }

    internal OrderStatusChangedEvent(Order order)
    {
        OrderId = order.OrderId;
        OrderStatus = order.Status;
    }

    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
}
