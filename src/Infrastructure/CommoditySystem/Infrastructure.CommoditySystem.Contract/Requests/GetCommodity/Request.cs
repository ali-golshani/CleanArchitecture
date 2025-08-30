using Framework.Mediator;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Requests.GetCommodity;

public class Request : RequestBase, IRequest<Request, Commodity?>
{
    public override string RequestTitle => "دریافت اطلاعات کالا";

    public required int CommodityId { get; init; }
}
