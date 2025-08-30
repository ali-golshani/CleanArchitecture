using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.MockData;

namespace Infrastructure.CommoditySystem.Requests;

internal sealed class VerifyCustomerCommodityLicenseRequestHandler : IRequestHandler<VerifyCustomerCommodityLicenseRequest, bool>
{
    public async Task<Result<bool>> Handle(VerifyCustomerCommodityLicenseRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = request.CustomerId;
        var commodityId = request.CommodityId;

        return
            Customers.IsValid(customerId) &&
            Commodities.IsValid(commodityId);
    }
}
