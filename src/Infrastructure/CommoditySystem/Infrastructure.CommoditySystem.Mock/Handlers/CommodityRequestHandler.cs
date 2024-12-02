using Framework.Mediator.Requests;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Mock.Handlers;

internal sealed class CommodityRequestHandler : IRequestHandler<CommodityRequest, Commodity?>
{
    public async Task<Result<Commodity?>> Handle(CommodityRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (Data.Commodities.IsValid(request.CommodityId))
        {
            return new Commodity(request.CommodityId, request.CommodityId.ToString());
        }
        else
        {
            return Result<Commodity?>.Success(null);
        }
    }
}
