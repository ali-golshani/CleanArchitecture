using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Persistence.Repositories;

internal class OrderRepository : Domain.Repositories.IOrderRepository
{
    private readonly OrderingDbContext db;

    public OrderRepository(OrderingDbContext db)
    {
        this.db = db;
    }

    public void Add(Domain.Order order)
    {
        db.Set<Domain.Order>().Add(order);
    }

    public async Task<bool> Exists(int orderId)
    {
        return await
            db.Set<Domain.Order>()
            .AnyAsync(x => x.OrderId == orderId);
    }

    public async Task<Domain.Order?> FindOrder(int orderId)
    {
        return await
            db.Set<Domain.Order>()
            .FirstOrDefaultAsync(x => x.OrderId == orderId);
    }
}
