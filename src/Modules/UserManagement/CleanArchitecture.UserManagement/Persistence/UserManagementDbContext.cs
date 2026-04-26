using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitecture.UserManagement.Persistence;

public sealed class UserManagementDbContext(DbContextOptions<UserManagementDbContext> options)
    : Framework.Persistence.DbContextBase(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}