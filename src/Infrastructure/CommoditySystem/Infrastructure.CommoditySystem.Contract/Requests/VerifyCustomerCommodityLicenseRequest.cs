using Framework.Mediator;

namespace Infrastructure.CommoditySystem.Requests;

public class VerifyCustomerCommodityLicenseRequest : RequestBase, IRequest<VerifyCustomerCommodityLicenseRequest, bool>
{
    public override string RequestTitle => "Verify Customer–Commodity License";
    public override bool? ShouldLog => true;

    public required int CustomerId { get; init; }
    public required int CommodityId { get; init; }
}
