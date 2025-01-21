using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Handlers;

internal sealed class CommodityRequestHandler : IRequestHandler<CommodityRequest, Commodity?>
{
    public async Task<Result<Commodity?>> Handle(CommodityRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (request.CommodityId <= 0)
        {
            return Result<Commodity?>.Success(null);
        }

        return new Commodity(request.CommodityId, request.CommodityId.ToString());
    }
}
