using Framework.Mediator;

namespace Infrastructure.CommoditySystem.Requests;

public class VerifyCustomerCommodityLicenseRequest : RequestBase, IRequest<VerifyCustomerCommodityLicenseRequest, bool>
{
    public override string RequestTitle => "اعتبار سنجی ارتباط مشتری و کالا";
    public override bool? ShouldLog => true;

    public required int CustomerId { get; init; }
    public required int CommodityId { get; init; }
}
