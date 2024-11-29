using Infrastructure.RequestAudit.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DbMigrationApp;

public class MigrateCleanDbService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public void AuditDbContext()
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

    public void OrderingDbContext()
    {
        Console.WriteLine("OrderingDbContext ...");
        using (var db = Service<Ordering.Persistence.OrderingDbContext>())
        {
            var cs = db.Database.GetConnectionString();
            PrintConnectionString(cs);
            db.Database.Migrate();
        }
        Console.WriteLine("Migration Finished .");
    }
}
