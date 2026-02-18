using CleanArchitecture.Ordering.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanArchitecture.Administration.DbMigrationApp.DesignTimeDbContext;

public sealed class OrderingDbContextFactory : IDesignTimeDbContextFactory<OrderingDbContext>
{
    public OrderingDbContext CreateDbContext(string[] args)
    {
        var schema = Settings.SchemaNames.Ordering;
        var optionsBuilder = new DbContextOptionsBuilder<OrderingDbContext>();
        SqlConfigs.Configure(optionsBuilder, schema);
        return new OrderingDbContext(optionsBuilder.Options);
    }
}
