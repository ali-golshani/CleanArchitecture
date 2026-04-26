using Infrastructure.RequestAudit.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RequestAudit.Persistence;

public class AuditDbContext : Framework.Persistence.DbContextBase
{
    public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AuditTrailConfiguration());
    }
}
