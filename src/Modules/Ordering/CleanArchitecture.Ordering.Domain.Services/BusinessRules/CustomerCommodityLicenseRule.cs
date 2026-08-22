namespace CleanArchitecture.Ordering.Domain.Services.BusinessRules;

internal sealed class CustomerCommodityLicenseRule : IBusinessRule
{
    public readonly struct Inquiry
    {
        public required readonly int CustomerId { get; init; }
        public required readonly int CommodityId { get; init; }
    }

    private readonly ICustomerCommodityLicenseVerifier licenseVerifier;
    private readonly Inquiry inquiry;

    public CustomerCommodityLicenseRule(ICustomerCommodityLicenseVerifier licenseVerifier, Inquiry inquiry)
    {
        this.licenseVerifier = licenseVerifier;
        this.inquiry = inquiry;
    }

    public async IAsyncEnumerable<Error> Evaluate()
    {
        var result = await licenseVerifier.Verify(
            inquiry.CustomerId,
            inquiry.CommodityId,
            default);

        if (result.IsFailure)
        {
            foreach (var error in result.Errors)
            {
                yield return error;
            }
        }
        else if (result.Value is CustomerCommodityLicenseStatus.Invalid)
        {
            yield return new Error
            (
                ErrorCodes.InvalidCustomerCommodityLicense,
                ErrorType.Conflict,
                Resources.RuleMessages.CustomerCommodityRelationRule,
                (nameof(Inquiry.CustomerId), inquiry.CustomerId),
                (nameof(Inquiry.CommodityId), inquiry.CommodityId)
            );
        }
    }
}
