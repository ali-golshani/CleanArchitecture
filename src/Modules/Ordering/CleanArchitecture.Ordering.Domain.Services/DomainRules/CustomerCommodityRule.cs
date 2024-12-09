using Framework.DomainRules.Extensions;
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
        var result = await commoditySystem.Handle(new CustomerCommodityValidationRequest
        {
            CustomerId = value.CustomerId,
            CommodityId = value.CommodityId,
        }, default);

        if (result.IsFailure)
        {
            foreach (var error in result.Errors)
            {
                yield return error.ToClause();
            }
        }
        else
        {
            yield return new Clause
            (
                result.Value,
                "ارتباط کالا و مشتری برقرار نیست",
                (nameof(Inquiry.CustomerId), value.CustomerId),
                (nameof(Inquiry.CommodityId), value.CommodityId)
            );
        }
    }
}
