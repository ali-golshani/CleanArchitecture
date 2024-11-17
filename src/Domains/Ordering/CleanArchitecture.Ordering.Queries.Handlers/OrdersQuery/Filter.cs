using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.Ordering.Queries.OrdersQuery;

internal sealed class Filter : IQueryFilter<Query>
{
    Query IQueryFilter<Query>.Filter(Actor? actor, Query query)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        return new Query
        {
            CommodityId = query.CommodityId,
            OrderStatus = query.OrderStatus,
            BrokerId = brokerId ?? query.BrokerId,
            CustomerId = customerId ?? query.CustomerId,
        };
    }
}
