using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.Models;
using Infrastructure.CommoditySystem.Requests.GetCommodity;

namespace Infrastructure.CommoditySystem.Mock.Requests.GetCommodity;

internal sealed class Handler : IRequestHandler<Infrastructure.CommoditySystem.Requests.GetCommodity.Request, Commodity?>
{
    public async Task<Result<Commodity?>> Handle(Infrastructure.CommoditySystem.Requests.GetCommodity.Request request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Data.Commodities.Find(request.CommodityId);
    }
}
