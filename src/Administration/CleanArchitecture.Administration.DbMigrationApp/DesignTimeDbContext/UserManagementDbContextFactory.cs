using CleanArchitecture.UserManagement.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CleanArchitecture.Administration.DbMigrationApp.DesignTimeDbContext;

public sealed class UserManagementDbContextFactory : IDesignTimeDbContextFactory<UserManagementDbContext>
{
    public UserManagementDbContext CreateDbContext(string[] args)
    {
        var schema = Settings.SchemaNames.UserManagement;
        var optionsBuilder = new DbContextOptionsBuilder<UserManagementDbContext>();
        SqlConfigs.Configure(optionsBuilder, schema);
        return new UserManagementDbContext(optionsBuilder.Options);
    }
}
