using CleanArchitecture.Mediator;

namespace CleanArchitecture.Ordering.Queries.Orders.GetOrder;

public class Query : QueryBase, IOrderRequest, IQuery<Query, Models.Order?>
{
    public override string RequestTitle => "Get Order Query";

    public int OrderId { get; init; }
}
