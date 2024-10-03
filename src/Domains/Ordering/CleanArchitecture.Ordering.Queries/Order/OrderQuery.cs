namespace CleanArchitecture.Ordering.Queries.Order;

public class OrderQuery :
    QueryBase,
    Mediator.IOrderRequest,
    IQuery<OrderQuery, Models.Order?>
{
    public override string RequestTitle => "Order Query";

    public int OrderId { get; init; }
}
