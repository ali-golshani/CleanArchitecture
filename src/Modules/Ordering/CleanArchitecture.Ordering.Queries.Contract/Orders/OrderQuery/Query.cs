using CleanArchitecture.Mediator;

namespace CleanArchitecture.Ordering.Queries.Orders.OrderQuery;

public class Query :
    QueryBase,
    IOrderRequest,
    IQuery<Query, Models.Order?>
{
    public override string RequestTitle => "Order Query";

    public int OrderId { get; init; }
}
