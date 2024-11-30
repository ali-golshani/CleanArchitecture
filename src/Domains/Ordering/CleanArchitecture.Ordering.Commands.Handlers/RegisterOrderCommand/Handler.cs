using CleanArchitecture.Ordering.Domain.Services;
using Framework.Results;
using Infrastructure.CommoditySystem;
using Framework.Results.Extensions;
using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator.DomainEvents;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Requests;

namespace CleanArchitecture.Ordering.Commands.RegisterOrderCommand;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    private readonly IOrderRepository orderRepository;
    private readonly IRegisterOrderService registerOrderService;
    private readonly ICommoditySystem commoditySystem;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly IIntegrationEventBus integrationEventBus;

    public Handler(
        IOrderRepository orderRepository,
        IRegisterOrderService registerOrderService,
        ICommoditySystem commoditySystem,
        IDomainEventPublisher eventPublisher,
        IIntegrationEventBus integrationEventBus)
    {
        this.orderRepository = orderRepository;
        this.registerOrderService = registerOrderService;
        this.commoditySystem = commoditySystem;
        this.eventPublisher = eventPublisher;
        this.integrationEventBus = integrationEventBus;
    }

    public async Task<Result<Empty>> Handle(Command request, CancellationToken cancellationToken)
    {
        if (await orderRepository.Exists(request.OrderId))
        {
            return new DuplicateError("سفارش", request.OrderId);
        }

        var commodityResult = await GetCommodity(request.CommodityId, cancellationToken);

        if (commodityResult.IsFailure)
        {
            return commodityResult.Errors;
        }

        var commodity = commodityResult.Value!;

        var orderResult = await registerOrderService.RegisterOrder(new RegisterOrderRequest
        {
            OrderId = request.OrderId,
            Quantity = request.Quantity,
            Price = request.Price,
            CustomerId = request.CustomerId,
            BrokerId = request.BrokerId,
            Commodity = new Domain.Commodity(commodity.CommodityId, commodity.CommodityName)
        });

        if (orderResult.IsFailure)
        {
            return orderResult.Errors;
        }

        var result = await eventPublisher.Publish(new OrderRegisteredEvent.Event { OrderId = request.OrderId });

        if (result.IsFailure)
        {
            return result;
        }

        await integrationEventBus.Post(new IntegrationEvents.OrderStatusChangedEvent
        {
            CorrelationId = request.CorrelationId,
            OrderId = request.OrderId,
            OrderStatus = orderResult.Value!.Status
        });

        return Empty.Value;
    }

    private async Task<Result<Commodity>> GetCommodity(int commodityId, CancellationToken cancellationToken)
    {
        var request = new CommodityRequest
        {
            CommodityId = commodityId
        };

        return await 
            commoditySystem
            .Handle(request, cancellationToken)
            .NotFoundIfNull("کالا", commodityId);
    }
}
