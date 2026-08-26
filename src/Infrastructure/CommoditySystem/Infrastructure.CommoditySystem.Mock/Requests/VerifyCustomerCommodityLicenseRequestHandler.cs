using Framework.Mediator;
using Framework.Results;
using Framework.Mediator.Middlewares;
using Infrastructure.CommoditySystem.Mock.MockData;
using Infrastructure.CommoditySystem.Requests;

namespace Infrastructure.CommoditySystem.Mock.Requests;

internal sealed class VerifyCustomerCommodityLicenseRequestHandler : IRequestHandler<VerifyCustomerCommodityLicenseRequest, bool>
{
    public async Task<Result<bool>> Handle(RequestContext<VerifyCustomerCommodityLicenseRequest> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;

        await Task.CompletedTask;

        var customerId = request.CustomerId;
        var commodityId = request.CommodityId;

        return
            Customers.IsValid(customerId) &&
            Commodities.IsValid(commodityId);
    }
}
