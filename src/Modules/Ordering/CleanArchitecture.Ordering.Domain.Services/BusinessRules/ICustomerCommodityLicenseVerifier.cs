namespace CleanArchitecture.Ordering.Domain.Services.BusinessRules;

public interface ICustomerCommodityLicenseVerifier
{
    Task<Result<CustomerCommodityLicenseStatus>> Verify(
        int customerId,
        int commodityId,
        CancellationToken cancellationToken);
}
