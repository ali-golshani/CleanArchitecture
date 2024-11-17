namespace CleanArchitecture.Ordering.Domain.Repositories;

public interface IOrderRepository
{
    Task<bool> Exists(int orderId);
    Task<Order?> FindOrder(int orderId);
    void Add(Order order);
}
