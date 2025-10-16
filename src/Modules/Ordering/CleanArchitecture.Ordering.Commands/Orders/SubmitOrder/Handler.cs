using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Ordering.Commands.Errors;
using CleanArchitecture.Ordering.DomainEvents;
using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator.DomainEvents;
using Framework.Results;
using Framework.Results.Errors;
using Framework.Mediator;

namespace CleanArchitecture.Ordering.Commands.Orders.SubmitOrder;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    private readonly IActorResolver actorResolver;
    private readonly IOrderRepository orderRepository;
    private readonly IDomainEventBus domainEventBus;

    public Handler(
        IActorResolver actorResolver,
        IOrderRepository orderRepository,
        IDomainEventBus domainEventBus)
    {
        this.actorResolver = actorResolver;
        this.orderRepository = orderRepository;
        this.domainEventBus = domainEventBus;
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
            await domainEventBus.Post(new OrderStatusChangedEvent
            {
                OrderId = order.OrderId,
                OrderStatus = order.Status,
                CorrelationId = request.CorrelationId,
            });
        }

        return Empty.Value;
    }
}
