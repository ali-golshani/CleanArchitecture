using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Handlers;

internal sealed class CustomerCommodityValidationRequestHandler : IRequestHandler<CustomerCommodityValidationRequest, bool>
{
    public async Task<Result<bool>> Handle(CustomerCommodityValidationRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var customerId = request.CustomerId;
        var commodityId = request.CommodityId;
        var result = commodityId > 0 && customerId > 0 && commodityId + customerId < 1000;

        return result;
    }
}
