using CleanArchitecture.Ordering.Domain.DomainRules;
using CleanArchitecture.Ordering.Domain.Exceptions;
using Framework.Domain;
using Framework.DomainRules.Extensions;

namespace CleanArchitecture.Ordering.Domain;

public class Order : CommandAwareEntity
{

#pragma warning disable CS8618

    private Order() { }

#pragma warning restore CS8618

    public Order(
        int orderId,
        int quantity,
        decimal price,
        int customerId,
        int brokerId,
        Commodity commodity,
        string trackingCode)
    {
        new IDomainRule[]
        {
            new OrderPriceRule(price),
            new OrderQuantityRule(quantity)
        }.Evaluate().Throw();

        OrderId = orderId;
        Quantity = quantity;
        Price = price;
        CustomerId = customerId;
        BrokerId = brokerId;
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

    public void UpdatePrice(decimal price)
    {
        new IDomainRule[]
        {
            new OrderPriceRule(price),
        }.Evaluate().Throw();

        Price = price;
    }

    public bool Submit()
    {
        switch (Status)
        {
            case OrderStatus.Draft:
                Status = OrderStatus.Submitted;
                return true;

            case OrderStatus.Submitted:
                return false;

            case OrderStatus.Approved:
                throw new SubmitApprovedOrderException(OrderId);

            case OrderStatus.Canceled:
                throw new SubmitCanceledOrderException(OrderId);

            default:
                throw new InvalidOrderStatusException(OrderId);
        }
    }
}
