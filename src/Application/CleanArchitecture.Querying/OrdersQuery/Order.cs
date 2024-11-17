namespace CleanArchitecture.Querying.OrdersQuery;

public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int BrokerId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int CommodityId { get; set; }
    public string? CommodityName { get; set; }
    public string? TrackingCode { get; set; }
    public OrderStatus Status { get; set; }
}
