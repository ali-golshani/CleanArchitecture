using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands.Errors;
using CleanArchitecture.Ordering.Commands.IntegrationEvents;
using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator.IntegrationEvents;
using Framework.Mediator.Requests;
using Framework.Results;
using Framework.Results.Errors;

namespace CleanArchitecture.Ordering.Commands.SubmitOrderCommand;

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

        var permission = await new AccessVerifier().IsAccessible(actor, order);

        if (!permission)
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
