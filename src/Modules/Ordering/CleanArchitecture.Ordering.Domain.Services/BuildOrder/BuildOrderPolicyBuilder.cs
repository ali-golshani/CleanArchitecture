using CleanArchitecture.Ordering.Domain.Orders.DomainRules;
using CleanArchitecture.Ordering.Domain.Services.BusinessRules;

namespace CleanArchitecture.Ordering.Domain.Services.BuildOrder;

internal sealed class BuildOrderPolicyBuilder(ICustomerCommodityLicenseVerifier licenseVerifier)
{
    private readonly ICustomerCommodityLicenseVerifier licenseVerifier = licenseVerifier;

    public BusinessPolicy Build(BuildOrderRequest value)
    {
        var domainRules = new IDomainRule[]
        {
            new OrderPriceRule(value.Price),
            new OrderQuantityRule(value.Quantity)
        };

        var businessRules = new IBusinessRule[]
        {
            new CustomerCommodityLicenseRule(licenseVerifier, new CustomerCommodityLicenseRule.Inquiry
            {
                CustomerId = value.CustomerId,
                CommodityId = value.Commodity.CommodityId,
            })
        };

        return new BusinessPolicy(domainRules, businessRules);
    }
}
