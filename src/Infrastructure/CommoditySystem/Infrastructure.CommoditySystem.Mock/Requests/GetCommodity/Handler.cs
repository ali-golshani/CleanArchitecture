using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.MockData;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Requests.GetCommodity;

internal sealed class Handler : IRequestHandler<Request, Commodity?>
{
    public async Task<Result<Commodity?>> Handle(Request request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Commodities.Find(request.CommodityId);
    }
}
