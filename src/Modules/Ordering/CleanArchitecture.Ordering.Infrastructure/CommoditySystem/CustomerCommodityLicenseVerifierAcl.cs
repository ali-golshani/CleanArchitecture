using CleanArchitecture.Ordering.Domain.Services.BusinessRules;
using Framework.Results;
using Infrastructure.CommoditySystem;
using Infrastructure.CommoditySystem.Requests;

namespace CleanArchitecture.Ordering.Infrastructure.CommoditySystem;

internal sealed class CustomerCommodityLicenseVerifierAcl(ICommoditySystem commoditySystem)
    : ICustomerCommodityLicenseVerifier
{
    public async Task<Result<CustomerCommodityLicenseStatus>> Verify(
        int customerId,
        int commodityId,
        CancellationToken cancellationToken)
    {
        var result = await commoditySystem.Handle(new VerifyCustomerCommodityLicenseRequest
        {
            CustomerId = customerId,
            CommodityId = commodityId,
        }, cancellationToken);

        if (result.IsFailure)
        {
            return result.AsFailure<CustomerCommodityLicenseStatus>();
        }

        return result.Value
            ? CustomerCommodityLicenseStatus.Valid
            : CustomerCommodityLicenseStatus.Invalid;
    }
}
