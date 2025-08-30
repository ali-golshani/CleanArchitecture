using Framework.Mediator;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Requests;

public class GetCommodityRequest : RequestBase, IRequest<GetCommodityRequest, Commodity?>
{
    public override string RequestTitle => "دریافت اطلاعات کالا";

    public required int CommodityId { get; init; }
}
