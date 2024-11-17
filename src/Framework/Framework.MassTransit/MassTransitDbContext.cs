using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Framework.MassTransit;

public sealed class MassTransitDbContext : DbContext
{
    public MassTransitDbContext(DbContextOptions<MassTransitDbContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Settings.Persistence.SchemaNames.MassTransit);
        modelBuilder.AddTransactionalOutboxEntities();
    }
}
