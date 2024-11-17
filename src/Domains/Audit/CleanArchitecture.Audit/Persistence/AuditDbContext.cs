using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Audit.Persistence;

public class AuditDbContext : Framework.Persistence.DbContextBase
{
    public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new CommandAuditTrailConfiguration());
        modelBuilder.ApplyConfiguration(new QueryAuditTrailConfiguration());
    }
}
