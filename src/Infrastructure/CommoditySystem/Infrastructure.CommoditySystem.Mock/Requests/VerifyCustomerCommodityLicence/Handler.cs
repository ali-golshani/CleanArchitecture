using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.Requests.VerifyCustomerCommodityLicence;

namespace Infrastructure.CommoditySystem.Mock.Requests.VerifyCustomerCommodityLicence;

internal sealed class Handler : IRequestHandler<Infrastructure.CommoditySystem.Requests.VerifyCustomerCommodityLicence.Request, bool>
{
    public async Task<Result<bool>> Handle(Infrastructure.CommoditySystem.Requests.VerifyCustomerCommodityLicence.Request request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = request.CustomerId;
        var commodityId = request.CommodityId;

        return
            Data.Customers.IsValid(customerId) &&
            Data.Commodities.IsValid(commodityId);
    }
}
