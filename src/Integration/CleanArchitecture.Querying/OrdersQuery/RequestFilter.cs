using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;

namespace CleanArchitecture.Querying.OrdersQuery;

internal sealed class RequestFilter : IFilter<Query, IQueryable<Order>>
{
    public Query Filter(Query request, Actor actor)
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
