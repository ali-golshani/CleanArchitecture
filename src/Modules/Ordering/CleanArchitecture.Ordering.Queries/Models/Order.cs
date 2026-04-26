namespace CleanArchitecture.Ordering.Queries.Models;

public sealed class Order
{
    public required int OrderId { get; init; }
    public required int CustomerId { get; init; }
    public required int BrokerId { get; init; }
    public required int Quantity { get; set; }
    public required decimal Price { get; set; }
    public required Commodity Commodity { get; init; }
    public required string TrackingCode { get; init; }
    public required OrderStatus Status { get; init; }
}
