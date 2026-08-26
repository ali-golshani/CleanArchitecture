namespace CleanArchitecture.Ordering.Queries.Orders.GetOrders;

public class Query : QueryBase, IQuery<Query, Framework.Queries.PaginatedItems<Models.Order>>
{
    public override string RequestTitle => "Get Orders";

    public int? CustomerId { get; set; }
    public int? BrokerId { get; set; }
    public int? CommodityId { get; init; }
    public OrderStatus? OrderStatus { get; init; }
    public Models.OrderOrderBy? OrderBy { get; init; }

    public int PageIndex { get; init; } = 0; 
    public int PageSize { get; init; } = 10;
}
