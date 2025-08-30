using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Requests.GetCommodity;

internal sealed class Handler : IRequestHandler<Request, Commodity?>
{
    public async Task<Result<Commodity?>> Handle(Request request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (request.CommodityId <= 0)
        {
            return Result<Commodity?>.Success(null);
        }

        return new Commodity(request.CommodityId, request.CommodityId.ToString());
    }
}
