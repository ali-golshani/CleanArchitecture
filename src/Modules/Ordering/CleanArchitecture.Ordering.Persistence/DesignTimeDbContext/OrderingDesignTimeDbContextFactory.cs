using Framework.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Ordering.Persistence.DesignTimeDbContext;

public sealed class OrderingDesignTimeDbContextFactory : SqlDesignTimeDbContextFactory<OrderingDbContext>
{
    protected override OrderingDbContext CreateDbContext(DbContextOptions<OrderingDbContext> options)
    {
        return new OrderingDbContext(options);
    }
}
