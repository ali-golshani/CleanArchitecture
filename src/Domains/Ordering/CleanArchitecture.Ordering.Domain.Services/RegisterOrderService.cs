using CleanArchitecture.Ordering.Domain.DomainRules;
using CleanArchitecture.Ordering.Domain.Repositories;
using CleanArchitecture.Ordering.Domain.Services.DomainRules;
using Framework.DomainRules.Extensions;

namespace CleanArchitecture.Ordering.Domain.Services;

internal class RegisterOrderService : IRegisterOrderService
{
    private readonly IOrderRepository repository;
    private readonly IOrderTrackingCodeBuilder trackingCodeBuilder;
    private readonly CustomerCommodityRule commodityValidationRule;

    public RegisterOrderService(
        IOrderRepository repository,
        IOrderTrackingCodeBuilder trackingCodeBuilder,
        CustomerCommodityRule commodityValidationRule)
    {
        this.repository = repository;
        this.trackingCodeBuilder = trackingCodeBuilder;
        this.commodityValidationRule = commodityValidationRule;
    }

    public async Task<Result<Order>> RegisterOrder(RegisterOrderRequest request)
    {
        var errors = new IDomainRule[]
        {
            new OrderPriceRule(request.Price),
            new OrderQuantityRule(request.Quantity),
        }.Evaluate().Errors().ToList();

        if (errors.Count > 0)
        {
            return errors;
        }

        errors = await
            commodityValidationRule
            .Evaluate(new CustomerCommodityRule.Inquiry
            {
                CustomerId = request.CustomerId,
                CommodityId = request.Commodity.CommodityId,
            })
            .Errors()
            .ToListAsync();

        if (errors.Count > 0)
        {
            return errors;
        }

        var trackingCode = trackingCodeBuilder.Build();

        var order = new Order
        (
            orderId: request.OrderId,
            quantity: request.Quantity,
            price: request.Price,
            commodity: request.Commodity,
            trackingCode: trackingCode
        );

        repository.Add(order);

        return order;
    }
}
