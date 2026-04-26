using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator;
using Framework.Results;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Queries.Orders.GetOrder;

internal sealed class Handler(IActorResolver actorResolver, IOrderingQueryDb db) : IRequestHandler<Query, Models.Order?>
{
    private readonly IActorResolver actorResolver = actorResolver;
    private readonly IOrderingQueryDb db = db;

    public async Task<Result<Models.Order?>> Handle(Query request, CancellationToken cancellationToken)
    {
        var actor = actorResolver.Actor;

        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        var order = await GetOrder(request.OrderId, customerId: customerId, brokerId: brokerId);
        return order?.Convert();
    }

    private async Task<Domain.Orders.Order?> GetOrder(int orderId, int? customerId, int? brokerId)
    {
        var set = db.QuerySet<Domain.Orders.Order>();

        if (customerId is not null)
        {
            set = set.Where(x => x.CustomerId == customerId.Value);
        }

        if (brokerId is not null)
        {
            set = set.Where(x => x.BrokerId == brokerId.Value);
        }

        return await set.FirstOrDefaultAsync(x => x.OrderId == orderId);
    }
}
