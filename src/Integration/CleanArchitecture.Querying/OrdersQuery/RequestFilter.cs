using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;

namespace CleanArchitecture.Querying.OrdersQuery;

internal sealed class RequestFilter : IRequestFilter<Query>
{
    public Query Filter(Actor? actor, Query request)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        return new Query
        {
            BrokerId = brokerId ?? request.BrokerId,
            CustomerId = customerId ?? request.CustomerId,
        };
    }
}
