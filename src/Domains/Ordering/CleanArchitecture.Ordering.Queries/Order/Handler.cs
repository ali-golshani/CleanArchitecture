using CleanArchitecture.Actors;
using CleanArchitecture.Mediator;
using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Results;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Queries.Order;

internal class Handler : IRequestHandler<OrderQuery, Models.Order?>
{
    private readonly IActorResolver actorResolver;
    private readonly IOrderQueryDb db;

    public Handler(IActorResolver actorResolver, IOrderQueryDb db)
    {
        this.actorResolver = actorResolver;
        this.db = db;
    }

    public async Task<Result<Models.Order?>> Handle(OrderQuery request, CancellationToken cancellationToken)
    {
        var actor = actorResolver.Actor;
        var order = await GetOrder(actor, request.OrderId);

        if (order is null)
        {
            return new NotFoundError("سفارش", request.OrderId);
        }

        return order.Convert();
    }

    private Task<Domain.Order?> GetOrder(Actor? actor, int orderId)
    {
        var query = new InternalQuery { OrderId = orderId };
        SetParameters(query, actor);
        return GetOrder(query);
    }

    private async Task<Domain.Order?> GetOrder(InternalQuery query)
    {
        IQueryable<Domain.Order> set = db.QuerySet<Domain.Order>();

        if (query.CustomerId is not null)
        {
            set = set.Where(x => x.CustomerId == query.CustomerId.Value);
        }

        if (query.BrokerId is not null)
        {
            set = set.Where(x => x.BrokerId == query.BrokerId.Value);
        }

        return await set.FirstOrDefaultAsync(x => x.OrderId == query.OrderId);
    }

    private static void SetParameters(InternalQuery query, Actor? actor)
    {
        switch (actor)
        {
            case CustomerActor customer:
                query.CustomerId = customer.CustomerId;
                break;

            case BrokerActor broker:
                query.BrokerId = broker.BrokerId;
                break;

            default:
                break;
        }
    }

    private sealed class InternalQuery : OrderQuery
    {
        public int? CustomerId { get; set; }
        public int? BrokerId { get; set; }
    }
}
