using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Mediator.Middlewares;
using CleanArchitecture.Ordering.Queries.Models;

namespace CleanArchitecture.Ordering.Queries.OrderQuery;

internal sealed class RequestFilter : IFilter<Query, Order?>
{
    Query IFilter<Query, Order?>.Filter(Query request, Actor actor)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        return new FilteredQuery
        {
            OrderId = request.OrderId,
            CustomerId = customerId,
            BrokerId = brokerId
        };
    }
}
