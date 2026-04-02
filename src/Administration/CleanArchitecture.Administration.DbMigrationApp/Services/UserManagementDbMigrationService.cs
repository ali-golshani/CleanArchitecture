using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class UserManagementDbMigrationService(IServiceProvider serviceProvider) : DbMigrationServiceBase(serviceProvider)
{
    public void Migrate()
    {
        Console.WriteLine("UserManagementDbContext ...");
        using (var db = Service<UserManagement.Persistence.UserManagementDbContext>())
        {
            PrintMigrationInfo(db.Database);
            db.Database.Migrate();
        }
        Console.WriteLine("Migration Finished .");
    }
}
