using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Ordering.Queries.Models;
using Framework.Queries;

namespace CleanArchitecture.Ordering.Queries.Orders.GetOrders;

internal sealed class RequestFilter : IFilter<Query, PaginatedItems<Order>>
{
    public void Filter(Query request, Actor actor)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        request.BrokerId = brokerId ?? request.BrokerId;
        request.CustomerId = customerId ?? request.CustomerId;
    }
}
