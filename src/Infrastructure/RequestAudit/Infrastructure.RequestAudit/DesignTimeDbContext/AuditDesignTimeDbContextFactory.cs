using Framework.Persistence;
using Infrastructure.RequestAudit.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RequestAudit.DesignTimeDbContext;

public sealed class AuditDesignTimeDbContextFactory : SqlDesignTimeDbContextFactory<AuditDbContext>
{
    protected override AuditDbContext CreateDbContext(DbContextOptions<AuditDbContext> options)
    {
        return new AuditDbContext(options);
    }
}
