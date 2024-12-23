using CleanArchitecture.Ordering.Domain.Orders;

namespace CleanArchitecture.Ordering.Domain.Services;

public interface IBuildOrderService
{
    Task<Result<Order>> BuildOrder(BuildOrderRequest request);
}