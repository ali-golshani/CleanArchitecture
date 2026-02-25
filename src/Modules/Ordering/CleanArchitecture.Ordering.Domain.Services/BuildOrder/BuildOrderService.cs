using CleanArchitecture.Ordering.Domain.Orders;
using Framework.DomainRules.Extensions;

namespace CleanArchitecture.Ordering.Domain.Services.BuildOrder;

internal class BuildOrderService : IBuildOrderService
{
    private readonly BuildOrderPolicyBuilder policyBuilder;
    private readonly IOrderTrackingCodeBuilder trackingCodeBuilder;

    public BuildOrderService(
        BuildOrderPolicyBuilder policyBuilder,
        IOrderTrackingCodeBuilder trackingCodeBuilder)
    {
        this.policyBuilder = policyBuilder;
        this.trackingCodeBuilder = trackingCodeBuilder;
    }

    public async Task<Result<Order>> BuildOrder(BuildOrderRequest request)
    {
        var policy = policyBuilder.Build(request);
        var errors = await policy.EvaluateAndReturnErrors();

        if (errors.Count > 0)
        {
            return errors;
        }

        var trackingCode = trackingCodeBuilder.Build();

        var parameters = new OrderCreationParameters
        {
            OrderId = request.OrderId,
            Quantity = request.Quantity,
            Price = request.Price,
            CustomerId = request.CustomerId,
            BrokerId = request.BrokerId,
            Commodity = request.Commodity,
            TrackingCode = trackingCode
        };

        return new Order(parameters);
    }
}
