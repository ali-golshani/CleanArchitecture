using CleanArchitecture.Ordering.Domain.Orders.DomainRules;
using CleanArchitecture.Ordering.Domain.Services.DomainRules;
using Infrastructure.CommoditySystem;

namespace CleanArchitecture.Ordering.Domain.Services.BuildOrder;

internal sealed class BuildOrderPolicyBuilder(ICommoditySystem commoditySystem)
{
    private readonly ICommoditySystem commoditySystem = commoditySystem;

    public BusinessPolicy Build(BuildOrderRequest value)
    {
        var domainRules = new IDomainRule[]
        {
            new OrderPriceRule(value.Price),
            new OrderQuantityRule(value.Quantity)
        };

        var businessRules = new IBusinessRule[]
        {
            new CustomerCommodityRule(commoditySystem, new CustomerCommodityRule.Inquiry
            {
                CustomerId = value.CustomerId,
                CommodityId = value.Commodity.CommodityId,
            })
        };

        return new BusinessPolicy(domainRules, businessRules);
    }
}
