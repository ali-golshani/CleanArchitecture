namespace CleanArchitecture.Ordering.Domain.Orders;

public sealed class OrderCreationParameters
{
    public required int OrderId { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
    public required int CustomerId { get; init; }
    public required int BrokerId { get; init; }
    public required Commodity Commodity { get; init; }
    public required string TrackingCode { get; init; }
}
