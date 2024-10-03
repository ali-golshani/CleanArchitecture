namespace CleanArchitecture.Ordering.Domain.DomainEvents;

public class OrderEvent : DomainEvent
{
    private OrderEvent() { }

    internal OrderEvent(Order order)
    {
        OrderId = order.OrderId;
        OrderStatus = order.Status;
    }

    public int OrderId { get; }
    public OrderStatus OrderStatus { get; }
}
