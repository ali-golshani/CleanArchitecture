using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Requests;

internal sealed class GetCommodityRequestHandler : IRequestHandler<GetCommodityRequest, Commodity?>
{
    public async Task<Result<Commodity?>> Handle(RequestContext<GetCommodityRequest> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;
        await Task.CompletedTask;

        if (request.CommodityId <= 0)
        {
            return Result<Commodity?>.Success(null);
        }

        return new Commodity(request.CommodityId, request.CommodityId.ToString());
    }
}
