using Framework.Messaging.MassTransit;
using Framework.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Framework.Messaging.MassTransit.DesignTimeDbContext;

public sealed class MassTransitDesignTimeDbContextFactory : SqlDesignTimeDbContextFactory<MassTransitDbContext>
{
    protected override MassTransitDbContext CreateDbContext(DbContextOptions<MassTransitDbContext> options)
    {
        return new MassTransitDbContext(options);
    }
}
