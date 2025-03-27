using Infrastructure.RequestAudit.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

public class AuditDbMigrationService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
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
