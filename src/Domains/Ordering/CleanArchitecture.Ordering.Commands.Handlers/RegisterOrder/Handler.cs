using CleanArchitecture.Mediator;
using CleanArchitecture.Ordering.Domain.Services;
using Framework.Results;
using Infrastructure.CommoditySystem;

namespace CleanArchitecture.Ordering.Commands.RegisterOrder;

internal class Handler : IRequestHandler<RegisterOrderCommand, Empty>
{
    private readonly IRegisterOrderService registerOrderService;
    private readonly ICommoditySystem commoditySystem;
    private readonly IDomainEventPublisher eventPublisher;

    public Handler(
        IRegisterOrderService registerOrderService,
        ICommoditySystem commoditySystem,
        IDomainEventPublisher eventPublisher)
    {
        this.registerOrderService = registerOrderService;
        this.commoditySystem = commoditySystem;
        this.eventPublisher = eventPublisher;
    }

    public async Task<Result<Empty>> Handle(RegisterOrderCommand request, CancellationToken cancellationToken)
    {
        var commodity = await commoditySystem.GetCommodity(request.CommodityId);

        if (commodity is null)
        {
            return new NotFoundError("کالا", request.CommodityId);
        }

        var result = await registerOrderService.RegisterOrder(new RegisterOrderRequest
        {
            OrderId = request.OrderId,
            Quantity = request.Quantity,
            Price = request.Price,
            CustomerId = request.CustomerId,
            Commodity = new Domain.Commodity(commodity.CommodityId, commodity.CommodityName)
        });

        if (result.IsFailure)
        {
            return result;
        }

        await eventPublisher.Publish(new OrderRegistered.OrderRegisteredEvent { OrderId = request.OrderId });

        return result;
    }
}
