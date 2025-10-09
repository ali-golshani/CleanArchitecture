namespace CleanArchitecture.Querying.GetOrders;

public class Query : QueryBase, IQuery<Query, IQueryable<Order>>
{
    public int? CustomerId { get; set; }
    public int? BrokerId { get; set; }

    public override string RequestTitle => "Get Orders";
}
