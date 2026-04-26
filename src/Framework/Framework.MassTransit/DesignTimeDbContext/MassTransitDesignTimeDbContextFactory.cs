using Framework.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Framework.MassTransit.DesignTimeDbContext;

public sealed class MassTransitDesignTimeDbContextFactory : SqlDesignTimeDbContextFactory<MassTransitDbContext>
{
    protected override MassTransitDbContext CreateDbContext(DbContextOptions<MassTransitDbContext> options)
    {
        return new MassTransitDbContext(options);
    }
}
