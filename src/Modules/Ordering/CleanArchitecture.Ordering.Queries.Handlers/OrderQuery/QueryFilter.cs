using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.Ordering.Queries.OrderQuery;

internal sealed class QueryFilter : IQueryFilter<Query>
{
    public Query Filter(Actor? actor, Query query)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        return new FilteredQuery
        {
            OrderId = query.OrderId,
            CustomerId = customerId,
            BrokerId = brokerId
        };
    }
}
