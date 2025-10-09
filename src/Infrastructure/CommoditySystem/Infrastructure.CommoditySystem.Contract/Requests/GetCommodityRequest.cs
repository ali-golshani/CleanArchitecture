using Framework.Mediator;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Requests;

public class GetCommodityRequest : RequestBase, IRequest<GetCommodityRequest, Commodity?>
{
    public override string RequestTitle => "Get Commodity";

    public required int CommodityId { get; init; }
}
