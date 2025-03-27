using Infrastructure.RequestAudit.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class AuditDbMigrationService(IServiceProvider serviceProvider) : DbMigrationServiceBase(serviceProvider)
{
    public void Migrate()
    {
        Console.WriteLine("AuditDbContext ...");
        using (var db = Service<AuditDbContext>())
        {
            var cs = db.Database.GetConnectionString();
            PrintConnectionString(cs);
            db.Database.Migrate();
        }
        Console.WriteLine("Migration Finished .");
    }
}
