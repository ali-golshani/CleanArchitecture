namespace CleanArchitecture.Ordering.Queries.Models;

public sealed class Order
{
    public required int OrderId { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}
