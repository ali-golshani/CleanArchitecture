using CleanArchitecture.Actors;
using CleanArchitecture.Mediator.Middlewares.Transformers;

namespace CleanArchitecture.Querying.OrdersQuery;

internal sealed class RequestFilter : FilteringTransformer<Query>
{
    public override int Order { get; } = 1;

    protected override Query Filter(Query value, Actor actor)
    {
        var customerId = (actor as CustomerActor)?.CustomerId;
        var brokerId = (actor as BrokerActor)?.BrokerId;

        return new Query
        {
            BrokerId = brokerId ?? value.BrokerId,
            CustomerId = customerId ?? value.CustomerId,
        };
    }
}
