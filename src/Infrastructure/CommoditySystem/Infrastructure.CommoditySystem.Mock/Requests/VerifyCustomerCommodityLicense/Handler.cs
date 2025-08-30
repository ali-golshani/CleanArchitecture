using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.MockData;

namespace Infrastructure.CommoditySystem.Requests.VerifyCustomerCommodityLicense;

internal sealed class Handler : IRequestHandler<Request, bool>
{
    public async Task<Result<bool>> Handle(Request request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = request.CustomerId;
        var commodityId = request.CommodityId;

        return
            Customers.IsValid(customerId) &&
            Commodities.IsValid(commodityId);
    }
}
