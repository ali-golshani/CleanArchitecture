using Framework.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.UserManagement.Persistence.DesignTimeDbContext;

public sealed class UserManagementDesignTimeDbContextFactory : SqlDesignTimeDbContextFactory<UserManagementDbContext>
{
    protected override UserManagementDbContext CreateDbContext(DbContextOptions<UserManagementDbContext> options)
    {
        return new UserManagementDbContext(options);
    }
}
