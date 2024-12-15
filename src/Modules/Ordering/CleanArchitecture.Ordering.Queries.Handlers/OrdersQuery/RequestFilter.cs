using CleanArchitecture.Actors;
using CleanArchitecture.Mediator.Middlewares.Transformers;

namespace CleanArchitecture.Ordering.Queries.OrdersQuery;

internal sealed class RequestFilter : FilteringTransformer<Query>
{
    public override int Order { get; } = 1;

    protected override Query Filter(Query value, Actor actor)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        return new Query
        {
            CommodityId = value.CommodityId,
            OrderStatus = value.OrderStatus,
            BrokerId = brokerId ?? value.BrokerId,
            CustomerId = customerId ?? value.CustomerId,
            OrderBy = value.OrderBy,
            PageIndex = value.PageIndex,
            PageSize = value.PageSize,
        };
    }
}
