namespace CleanArchitecture.Ordering.Queries.OrdersQuery;

public class Query :
    QueryBase,
    IQuery<Query, Framework.Queries.PaginatedItems<Models.Order>>
{
    public override string RequestTitle => "Orders Query";

    public int? CustomerId { get; init; }
    public int? BrokerId { get; init; }
    public int? CommodityId { get; init; }
    public OrderStatus? OrderStatus { get; init; }

    public int PageIndex { get; init; } = 0; 
    public int PageSize { get; init; } = 10;
}
