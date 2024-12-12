using CleanArchitecture.Ordering.Domain.DomainRules;
using CleanArchitecture.Ordering.Domain.Services.DomainRules;
using Framework.DomainRules.Extensions;
using Framework.DomainRules.Policies;

namespace CleanArchitecture.Ordering.Domain.Services;

internal class BuildOrderPolicyBuilder : IPolicyBuilder<BuildOrderRequest>
{
    private readonly CustomerCommodityRule customerCommodityRule;

    public BuildOrderPolicyBuilder(CustomerCommodityRule customerCommodityRule)
    {
        this.customerCommodityRule = customerCommodityRule;
    }

    public Policy Build(BuildOrderRequest value)
    {
        var rules = new IDomainRule[]
        {
            new OrderPriceRule(value.Price),
            new OrderQuantityRule(value.Quantity)
        };

        var asyncRules = new IAsyncDomainRule[]
        {
            customerCommodityRule
            .AsNonGeneric(new CustomerCommodityRule.Inquiry
            {
                CustomerId = value.CustomerId,
                CommodityId = value.Commodity.CommodityId,
            })
        };

        return new Policy(rules, asyncRules);
    }
}
