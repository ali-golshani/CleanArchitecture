using CleanArchitecture.Audit.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanArchitecture.Administration.DbMigrationApp.DesignTimeDbContext;

public class AuditDbContextFactory : IDesignTimeDbContextFactory<AuditDbContext>
{
    public AuditDbContext CreateDbContext(string[] args)
    {
        var schema = Audit.Settings.Persistence.SchemaNames.Audit;
        var optionsBuilder = new DbContextOptionsBuilder<AuditDbContext>();
        SqlConfigs.Configure(optionsBuilder, schema);
        return new AuditDbContext(optionsBuilder.Options);
    }
}
