using CleanArchitecture.Ordering.Domain.DomainRules;
using CleanArchitecture.Ordering.Domain.Services.DomainRules;
using Infrastructure.CommoditySystem;

namespace CleanArchitecture.Ordering.Domain.Services;

internal class BuildOrderPolicyBuilder
{
    private readonly ICommoditySystem commoditySystem;

    public BuildOrderPolicyBuilder(ICommoditySystem commoditySystem)
    {
        this.commoditySystem = commoditySystem;
    }

    public DomainPolicy Build(BuildOrderRequest value)
    {
        var rules = new IDomainRule[]
        {
            new OrderPriceRule(value.Price),
            new OrderQuantityRule(value.Quantity)
        };

        var asyncRules = new IAsyncDomainRule[]
        {
            new CustomerCommodityRule(commoditySystem, new CustomerCommodityRule.Inquiry
            {
                CustomerId = value.CustomerId,
                CommodityId = value.Commodity.CommodityId,
            })
        };

        return new DomainPolicy(rules, asyncRules);
    }
}
