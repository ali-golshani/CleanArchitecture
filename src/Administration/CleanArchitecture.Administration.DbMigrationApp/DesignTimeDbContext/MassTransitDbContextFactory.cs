using Framework.MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanArchitecture.Administration.DbMigrationApp.DesignTimeDbContext;

public sealed class MassTransitDbContextFactory : IDesignTimeDbContextFactory<MassTransitDbContext>
{
    public MassTransitDbContext CreateDbContext(string[] args)
    {
        var schema = Settings.Persistence.SchemaNames.MassTransit;
        var optionsBuilder = new DbContextOptionsBuilder<MassTransitDbContext>();
        SqlConfigs.Configure(optionsBuilder, schema);
        return new MassTransitDbContext(optionsBuilder.Options);
    }
}
