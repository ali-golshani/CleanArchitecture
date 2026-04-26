using CleanArchitecture.Ordering.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Persistence;

public class OrderingDbContext : Framework.Persistence.DbContextBase, IOrderingQueryDb
{
    public OrderingDbContext(DbContextOptions<OrderingDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderingDbContext).Assembly);
    }
}
