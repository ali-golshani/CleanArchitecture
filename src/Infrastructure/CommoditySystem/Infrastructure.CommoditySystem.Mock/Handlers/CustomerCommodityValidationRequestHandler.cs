using Framework.Mediator.Requests;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Mock.Handlers;

internal sealed class CustomerCommodityValidationRequestHandler : IRequestHandler<CustomerCommodityValidationRequest, bool>
{
    public async Task<Result<bool>> Handle(CustomerCommodityValidationRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = request.CustomerId;
        var commodityId = request.CommodityId;

        return
            Data.Customers.IsValid(customerId) &&
            Data.Commodities.IsValid(commodityId);
    }
}
