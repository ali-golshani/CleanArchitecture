using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands.Errors;
using CleanArchitecture.Ordering.IntegrationEvents;
using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator.IntegrationEvents;
using Framework.Results;
using Framework.Mediator;
using CleanArchitecture.Authorization.Extensions;

namespace CleanArchitecture.Ordering.Commands.Orders.SubmitOrder;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    private readonly IActorResolver actorResolver;
    private readonly IOrderRepository orderRepository;
    private readonly IIntegrationEventCollector integrationEvents;

    public Handler(
        IActorResolver actorResolver,
        IOrderRepository orderRepository,
        IIntegrationEventCollector integrationEvents)
    {
        this.actorResolver = actorResolver;
        this.orderRepository = orderRepository;
        this.integrationEvents = integrationEvents;
    }

    public async Task<Result<Empty>> Handle(RequestContext<Command> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;
        var actor = actorResolver.Actor;
        var order = await orderRepository.FindOrder(request.OrderId);

        if (order == null)
        {
            return new OrderNotFoundError(request.OrderId);
        }

        if (await new AccessControl().IsAccessDenied(actor, order))
        {
            return Framework.Results.Errors.Forbidden;
        }

        if (order.Submit())
        {
            integrationEvents.Add(new OrderStatusChangedEvent
            {
                CorrelationId = request.CorrelationId,
                OrderId = order.OrderId,
                OrderStatus = order.Status,
            });
        }

        return Empty.Value;
    }
}
