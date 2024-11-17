using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DbMigrationApp;

public class MigrateDbApp : BasicApp
{
    public void AuditDbContext()
    {
        Console.WriteLine("AuditDbContext ...");
        using (var db = Service<Audit.Persistence.AuditDbContext>())
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
