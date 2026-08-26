using CleanArchitecture.Ordering.Commands.Errors;
using CleanArchitecture.Ordering.Domain.Repositories;
using CleanArchitecture.Ordering.Domain.Services;
using Framework.Mediator;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Notifications;
using Framework.Results;
using Framework.Mediator.Middlewares;
using Framework.Results.Extensions;

namespace CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    private readonly IOrderRepository orderRepository;
    private readonly IBuildOrderService buildOrderService;
    private readonly ICommodityCatalog commodityCatalog;
    private readonly INotificationPublisher notificationPublisher;
    private readonly IIntegrationEventCollector integrationEvents;

    public Handler(
        IOrderRepository orderRepository,
        IBuildOrderService buildOrderService,
        ICommodityCatalog commodityCatalog,
        INotificationPublisher notificationPublisher,
        IIntegrationEventCollector integrationEvents)
    {
        this.orderRepository = orderRepository;
        this.buildOrderService = buildOrderService;
        this.commodityCatalog = commodityCatalog;
        this.notificationPublisher = notificationPublisher;
        this.integrationEvents = integrationEvents;
    }

    public async Task<Result<Empty>> Handle(RequestContext<Command> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;
        if (await orderRepository.Exists(request.OrderId))
        {
            return new DuplicateOrderError(request.OrderId);
        }

        var commodityResult = await GetCommodity(request.CommodityId, cancellationToken);

        if (commodityResult.IsFailure)
        {
            return commodityResult.AsFailure<Empty>();
        }

        var commodity = commodityResult.Value!;

        var orderResult = await BuildOrder(request, commodity, cancellationToken);

        if (orderResult.IsFailure)
        {
            return orderResult.AsFailure<Empty>();
        }

        var order = orderResult.Value!;

        orderRepository.Add(order);

        return await OnOrderRegistered(order, request.CorrelationId, cancellationToken);
    }

    private async Task<Result<Domain.Orders.Commodity>> GetCommodity(int commodityId, CancellationToken cancellationToken)
    {
        return await
            commodityCatalog.Find(commodityId, cancellationToken)
            .NotFoundIfNull(new CommodityNotFoundError(commodityId));
    }

    private Task<Result<Domain.Orders.Order>> BuildOrder(
        Command request,
        Domain.Orders.Commodity commodity,
        CancellationToken cancellationToken)
    {
        return buildOrderService.BuildOrder(new BuildOrderRequest
        {
            OrderId = request.OrderId,
            Quantity = request.Quantity,
            Price = request.Price,
            CustomerId = request.CustomerId,
            BrokerId = request.BrokerId,
            Commodity = commodity
        }, cancellationToken);
    }

    private async Task<Result<Empty>> OnOrderRegistered(
        Domain.Orders.Order order,
        Guid? correlationId,
        CancellationToken cancellationToken)
    {
        var result = await notificationPublisher.Publish
        (
            new Notifications.OrderRegistered.Notification { OrderId = order.OrderId },
            cancellationToken
        );

        if (result.IsFailure)
        {
            return result;
        }

        integrationEvents.Add(new IntegrationEvents.OrderStatusChangedEvent
        {
            CorrelationId = correlationId,
            OrderId = order.OrderId,
            OrderStatus = order.Status
        });

        return Empty.Value;
    }
}
