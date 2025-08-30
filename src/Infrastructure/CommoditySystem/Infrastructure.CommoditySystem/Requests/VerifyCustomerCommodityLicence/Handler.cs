using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Requests.VerifyCustomerCommodityLicence;

internal sealed class Handler : IRequestHandler<Request, bool>
{
    public async Task<Result<bool>> Handle(Request request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = request.CustomerId;
        var commodityId = request.CommodityId;
        var result = commodityId > 0 && customerId > 0 && commodityId + customerId < 1000;

        return result;
    }
}
