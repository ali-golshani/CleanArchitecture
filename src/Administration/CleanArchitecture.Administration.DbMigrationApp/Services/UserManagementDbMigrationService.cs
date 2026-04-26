using CleanArchitecture.UserManagement.Persistence;
using Framework.Persistence;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class UserManagementDbMigrationService(IServiceProvider serviceProvider)
    : DbMigrationService<UserManagementDbContext>(serviceProvider);
