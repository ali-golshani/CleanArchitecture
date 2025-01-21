using Framework.Mediator;

namespace Infrastructure.CommoditySystem;

public class CommodityRequest : RequestBase, IRequest<CommodityRequest, Commodity?>
{
    public override string RequestTitle => "دریافت اطلاعات کالا";

    public required int CommodityId { get; init; }
}
