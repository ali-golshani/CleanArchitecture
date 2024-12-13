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
        var errors = await policy.Errors();

        if (errors.Count > 0)
        {
            return errors;
        }

        var trackingCode = trackingCodeBuilder.Build();

        var order = new Order
        (
            orderId: request.OrderId,
            quantity: request.Quantity,
            price: request.Price,
            customerId: request.CustomerId,
            brokerId: request.BrokerId,
            commodity: request.Commodity,
            trackingCode: trackingCode
        );

        return order;
    }
}
