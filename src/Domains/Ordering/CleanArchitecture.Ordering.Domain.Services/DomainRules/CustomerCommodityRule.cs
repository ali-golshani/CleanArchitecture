using Infrastructure.CommoditySystem;

namespace CleanArchitecture.Ordering.Domain.Services.DomainRules;

internal class CustomerCommodityRule(ICommoditySystem commoditySystem) : IAsyncDomainRule<CustomerCommodityRule.Inquiry>
{
    public readonly struct Inquiry
    {
        public required int CustomerId { get; init; }
        public required int CommodityId { get; init; }
    }

    private readonly ICommoditySystem commoditySystem = commoditySystem;

    public async IAsyncEnumerable<Clause> Evaluate(Inquiry value)
    {
        var isValid = await commoditySystem.ValidateCustomerCommodity(value.CustomerId, value.CommodityId);

        yield return new Clause
        (
            isValid,
            "ارتباط کالا و مشتری برقرار نیست",
            (nameof(Inquiry.CustomerId), value.CustomerId),
            (nameof(Inquiry.CommodityId), value.CommodityId)
        );
    }
}
