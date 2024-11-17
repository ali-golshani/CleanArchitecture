using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Domain.Repositories;
using Framework.Mediator.Requests;
using Framework.Results;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Queries.OrderQuery;

internal sealed class Handler : IRequestHandler<Query, Models.Order?>
{
    private readonly IActorResolver actorResolver;
    private readonly IOrderingQueryDb db;

    public Handler(IActorResolver actorResolver, IOrderingQueryDb db)
    {
        this.actorResolver = actorResolver;
        this.db = db;
    }

    public async Task<Result<Models.Order?>> Handle(Query request, CancellationToken cancellationToken)
    {
        var actor = actorResolver.Actor;
        var query = Filter(request, actor);
        var order = await GetOrder(query);
        return order?.Convert();
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

    private static InternalQuery Filter(Query query, Actor? actor)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        return new InternalQuery
        {
            OrderId = query.OrderId,
            CustomerId = customerId,
            BrokerId = brokerId
        };
    }

    private sealed class InternalQuery : Query
    {
        public required int? CustomerId { get; init; }
        public required int? BrokerId { get; init; }
    }
}
