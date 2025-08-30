using Framework.Mediator;

namespace Infrastructure.CommoditySystem.Requests.VerifyCustomerCommodityLicense;

public class Request : RequestBase, IRequest<Request, bool>
{
    public override string RequestTitle => "اعتبار سنجی ارتباط مشتری و کالا";
    public override bool? ShouldLog => true;

    public required int CustomerId { get; init; }
    public required int CommodityId { get; init; }
}
