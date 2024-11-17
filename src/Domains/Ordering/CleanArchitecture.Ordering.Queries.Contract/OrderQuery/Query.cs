namespace CleanArchitecture.Ordering.Queries.OrderQuery;

public class Query :
    QueryBase,
    Mediator.IOrderRequest,
    IQuery<Query, Models.Order?>
{
    public override string RequestTitle => "Order Query";

    public int OrderId { get; init; }
}
