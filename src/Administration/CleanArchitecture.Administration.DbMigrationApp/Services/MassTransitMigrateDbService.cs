using MassTransit.SqlTransport;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DbMigrationApp;

public class MassTransitMigrateDbService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public void MassTransitDbContext()
    {
        Console.WriteLine("MassTransitDbContext ...");
        using (var db = Service<Framework.MassTransit.MassTransitDbContext>())
        {
            var cs = db.Database.GetConnectionString();
            PrintConnectionString(cs);
            db.Database.Migrate();
        }
        Console.WriteLine("Migration Finished .");
    }

    public void MassTransitSqlTransport()
    {
        Console.WriteLine("MassTransitSqlTransport ...");
        Service<SqlTransportMigrationHostedService>().StartAsync(default).Wait();
        Console.WriteLine("Migration Finished .");
    }
}
