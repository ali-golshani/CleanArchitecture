using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.Querying.OrdersQuery;

internal sealed class QueryFilter : IQueryFilter<Query>
{
    public Query Filter(Actor? actor, Query query)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        return new Query
        {
            BrokerId = brokerId ?? query.BrokerId,
            CustomerId = customerId ?? query.CustomerId,
        };
    }
}
