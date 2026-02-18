using Infrastructure.RequestAudit;
using Infrastructure.RequestAudit.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanArchitecture.Administration.DbMigrationApp.DesignTimeDbContext;

public sealed class AuditDbContextFactory : IDesignTimeDbContextFactory<AuditDbContext>
{
    public AuditDbContext CreateDbContext(string[] args)
    {
        var schema = Settings.Persistence.SchemaNames.Audit;
        var optionsBuilder = new DbContextOptionsBuilder<AuditDbContext>();
        SqlConfigs.Configure(optionsBuilder, schema);
        return new AuditDbContext(optionsBuilder.Options);
    }
}
