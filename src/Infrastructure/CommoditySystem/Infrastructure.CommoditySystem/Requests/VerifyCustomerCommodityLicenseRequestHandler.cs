using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Requests;

internal sealed class VerifyCustomerCommodityLicenseRequestHandler : IRequestHandler<VerifyCustomerCommodityLicenseRequest, bool>
{
    public async Task<Result<bool>> Handle(VerifyCustomerCommodityLicenseRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = request.CustomerId;
        var commodityId = request.CommodityId;
        var result = commodityId > 0 && customerId > 0 && commodityId + customerId < 1000;

        return result;
    }
}
