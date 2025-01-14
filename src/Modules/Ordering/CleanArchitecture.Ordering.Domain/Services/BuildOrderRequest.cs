﻿using CleanArchitecture.Ordering.Domain.Orders;

namespace CleanArchitecture.Ordering.Domain.Services;

public sealed class BuildOrderRequest
{
    public required int OrderId { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
    public required int CustomerId { get; init; }
    public required int BrokerId { get; init; }
    public required Commodity Commodity { get; init; }
}
