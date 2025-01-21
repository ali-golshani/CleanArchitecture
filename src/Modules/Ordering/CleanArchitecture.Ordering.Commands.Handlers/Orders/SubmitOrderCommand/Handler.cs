using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Ordering.Commands.Errors;
using CleanArchitecture.Ordering.IntegrationEvents;
using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator.IntegrationEvents;
using Framework.Results;
using Framework.Results.Errors;
using Framework.Mediator;

namespace CleanArchitecture.Ordering.Commands.Orders.SubmitOrderCommand;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    private readonly IActorResolver actorResolver;
    private readonly IOrderRepository orderRepository;
    private readonly IIntegrationEventBus integrationEventBus;

    public Handler(
        IActorResolver actorResolver,
        IOrderRepository orderRepository,
        IIntegrationEventBus integrationEventBus)
    {
        this.actorResolver = actorResolver;
        this.orderRepository = orderRepository;
        this.integrationEventBus = integrationEventBus;
    }

    public async Task<Result<Empty>> Handle(Command request, CancellationToken cancellationToken)
    {
        var actor = actorResolver.Actor;
        var order = await orderRepository.FindOrder(request.OrderId);

        if (order == null)
        {
            return new OrderNotFoundError(request.OrderId);
        }

        if (await new AccessControl().IsAccessDenied(actor, order))
        {
            return ForbiddenError.Default;
        }

        if (order.Submit())
        {
            await integrationEventBus.Post(new OrderStatusChangedEvent
            {
                OrderId = order.OrderId,
                OrderStatus = order.Status,
                CorrelationId = request.CorrelationId,
            });
        }

        return Empty.Value;
    }
}
