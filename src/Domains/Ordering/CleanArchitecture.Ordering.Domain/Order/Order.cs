using CleanArchitecture.Ordering.Domain.IntegrationEvents;
using CleanArchitecture.Ordering.Domain.DomainRules;

namespace CleanArchitecture.Ordering.Domain;

public class Order : CommandAwareEntity
{
    public Order(int orderId, int quantity, decimal price, Commodity commodity, string trackingCode)
    {
        new IDomainRule[]
        {
            new OrderPriceRule(price),
            new OrderQuantityRule(quantity)
        }.Evaluate().Throw();

        OrderId = orderId;
        Quantity = quantity;
        Price = price;
        Commodity = commodity;
        TrackingCode = trackingCode;

        Status = OrderStatus.Draft;
    }

    public int OrderId { get; }
    public int CustomerId { get; }
    public int BrokerId { get; }
    public int Quantity { get; private set; }
    public decimal Price { get; private set; }
    public Commodity Commodity { get; }
    public string TrackingCode { get; }
    public OrderStatus Status { get; private set; }

    public ICollection<OrderStatusChangedEvent> Events { get; } = [];

    public void UpdatePrice(decimal price)
    {
        new IDomainRule[]
        {
            new OrderPriceRule(price),
        }.Evaluate().Throw();

        Price = price;
    }

    public void Approve()
    {
        if (Status == OrderStatus.Approved)
        {
            return;
        }

        Status = OrderStatus.Approved;
        RaiseEvent();
    }

    private void RaiseEvent()
    {
        Events.Add(new OrderStatusChangedEvent(this));
    }
}
