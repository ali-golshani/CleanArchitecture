using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Persistence.Repositories;

internal class OrderRepository : Domain.Repositories.IOrderRepository
{
    private readonly OrderDbContext db;

    public OrderRepository(OrderDbContext db)
    {
        this.db = db;
    }

    public void Add(Domain.Order order)
    {
        db.Set<Domain.Order>().Add(order);
    }

    public async Task<Domain.Order?> FindOrder(int orderId)
    {
        return await
            db.Set<Domain.Order>()
            .FirstOrDefaultAsync(x => x.OrderId == orderId);
    }
}
