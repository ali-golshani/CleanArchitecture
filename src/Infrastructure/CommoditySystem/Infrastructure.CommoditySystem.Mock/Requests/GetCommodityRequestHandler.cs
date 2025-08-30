using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.MockData;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Requests;

internal sealed class GetCommodityRequestHandler : IRequestHandler<GetCommodityRequest, Commodity?>
{
    public async Task<Result<Commodity?>> Handle(GetCommodityRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Commodities.Find(request.CommodityId);
    }
}
