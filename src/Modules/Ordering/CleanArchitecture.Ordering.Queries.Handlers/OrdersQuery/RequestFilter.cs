using CleanArchitecture.Actors;
using CleanArchitecture.Authorization;
using CleanArchitecture.Ordering.Queries.Models;
using Framework.Queries;

namespace CleanArchitecture.Ordering.Queries.OrdersQuery;

internal sealed class RequestFilter : IFilter<Query, PaginatedItems<Order>>
{
    public Query Filter(Query request, Actor actor)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        return new Query
        {
            CommodityId = request.CommodityId,
            OrderStatus = request.OrderStatus,
            BrokerId = brokerId ?? request.BrokerId,
            CustomerId = customerId ?? request.CustomerId,
            OrderBy = request.OrderBy,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
        };
    }
}
