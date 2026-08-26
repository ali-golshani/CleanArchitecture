using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.Mock.MockData;
using Infrastructure.CommoditySystem.Models;
using Infrastructure.CommoditySystem.Requests;

namespace Infrastructure.CommoditySystem.Mock.Requests;

internal sealed class GetCommodityRequestHandler : IRequestHandler<GetCommodityRequest, Commodity?>
{
    public async Task<Result<Commodity?>> Handle(RequestContext<GetCommodityRequest> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;
        await Task.CompletedTask;
        return Commodities.Find(request.CommodityId);
    }
}
