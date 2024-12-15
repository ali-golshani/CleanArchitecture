using CleanArchitecture.Ordering.Commands.Errors;
using CleanArchitecture.Ordering.Domain.Repositories;
using CleanArchitecture.Ordering.Domain.Services;
using Framework.Mediator.DomainEvents;
using Framework.Mediator.Extensions;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Requests;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.RegisterOrderCommand;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    private readonly IOrderRepository orderRepository;
    private readonly IBuildOrderService buildOrderService;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly IIntegrationEventBus integrationEventBus;

    public Handler(
        IOrderRepository orderRepository,
        IBuildOrderService buildOrderService,
        IDomainEventPublisher eventPublisher,
        IIntegrationEventBus integrationEventBus)
    {
        this.orderRepository = orderRepository;
        this.buildOrderService = buildOrderService;
        this.eventPublisher = eventPublisher;
        this.integrationEventBus = integrationEventBus;
    }

    public Task<Result<Empty>> Handle(Command request, CancellationToken cancellationToken)
    {
        return Handle(request.AsRequestType<EnrichedCommand>(), cancellationToken);
    }

    private async Task<Result<Empty>> Handle(EnrichedCommand request, CancellationToken cancellationToken)
    {
        if (await orderRepository.Exists(request.OrderId))
        {
            return new DuplicateOrderError(request.OrderId);
        }

        var commodity = request.Commodity;

        var orderResult = await buildOrderService.BuildOrder(new BuildOrderRequest
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

        var order = orderResult.Value!;

        orderRepository.Add(order);

        var result = await eventPublisher.Publish
        (
            new OrderRegisteredEvent.Event { OrderId = request.OrderId },
            cancellationToken
        );

        if (result.IsFailure)
        {
            return result;
        }

        await integrationEventBus.Post(new IntegrationEvents.OrderStatusChangedEvent
        {
            CorrelationId = request.CorrelationId,
            OrderId = order.OrderId,
            OrderStatus = order.Status
        });

        return Empty.Value;
    }
}
