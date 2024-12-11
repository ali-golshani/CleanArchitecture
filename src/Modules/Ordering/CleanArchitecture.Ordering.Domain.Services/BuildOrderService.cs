using CleanArchitecture.Ordering.Domain.DomainRules;
using CleanArchitecture.Ordering.Domain.Services.DomainRules;
using Framework.DomainRules.Extensions;

namespace CleanArchitecture.Ordering.Domain.Services;

internal class BuildOrderService : IBuildOrderService
{
    private readonly IOrderTrackingCodeBuilder trackingCodeBuilder;
    private readonly CustomerCommodityRule commodityValidationRule;

    public BuildOrderService(
        IOrderTrackingCodeBuilder trackingCodeBuilder,
        CustomerCommodityRule commodityValidationRule)
    {
        this.trackingCodeBuilder = trackingCodeBuilder;
        this.commodityValidationRule = commodityValidationRule;
    }

    public async Task<Result<Order>> BuildOrder(BuildOrderRequest request)
    {
        var policy =
            Policy.Empty +
            new OrderPriceRule(request.Price) +
            new OrderQuantityRule(request.Quantity) +
            commodityValidationRule
            .AsNonGeneric(new CustomerCommodityRule.Inquiry
            {
                CustomerId = request.CustomerId,
                CommodityId = request.Commodity.CommodityId,
            });

        var errors = await policy.Evaluate().Errors().ToListAsync();

        if (errors.Count > 0)
        {
            return errors;
        }

        errors = new IDomainRule[]
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
            customerId: request.CustomerId,
            brokerId: request.BrokerId,
            commodity: request.Commodity,
            trackingCode: trackingCode
        );

        return order;
    }
}
