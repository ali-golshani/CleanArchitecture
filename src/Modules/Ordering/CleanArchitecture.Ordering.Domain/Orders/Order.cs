using CleanArchitecture.Ordering.Domain.Exceptions;
using CleanArchitecture.Ordering.Domain.Orders.DomainRules;
using Framework.Domain;
using Framework.DomainRules.Extensions;

namespace CleanArchitecture.Ordering.Domain.Orders;

public sealed class Order : CommandAwareEntity
{

#pragma warning disable CS8618

    private Order() { }

#pragma warning restore CS8618

    public Order(OrderCreationParameters parameters)
    {
        new IDomainRule[]
        {
            new OrderPriceRule(parameters.Price),
            new OrderQuantityRule(parameters.Quantity)
        }.Evaluate().Throw();

        OrderId = parameters.OrderId;
        Quantity = parameters.Quantity;
        Price = parameters.Price;
        CustomerId = parameters.CustomerId;
        BrokerId = parameters.BrokerId;
        Commodity = parameters.Commodity;
        TrackingCode = parameters.TrackingCode;

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
        new OrderPriceRule(price).Evaluate().Throw();

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
