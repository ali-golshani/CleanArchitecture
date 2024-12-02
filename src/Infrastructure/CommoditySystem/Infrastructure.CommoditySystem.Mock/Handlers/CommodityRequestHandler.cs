using Framework.Mediator.Requests;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Mock.Handlers;

internal sealed class CommodityRequestHandler : IRequestHandler<CommodityRequest, Commodity?>
{
    public async Task<Result<Commodity?>> Handle(CommodityRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Data.Commodities.Find(request.CommodityId);
    }
}
