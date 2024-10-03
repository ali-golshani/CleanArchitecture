namespace CleanArchitecture.Ordering.Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> FindOrder(int orderId);
    void Add(Order order);
}
