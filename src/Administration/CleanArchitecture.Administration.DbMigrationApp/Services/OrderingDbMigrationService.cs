using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

public class OrderingDbMigrationService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public void Migrate()
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
