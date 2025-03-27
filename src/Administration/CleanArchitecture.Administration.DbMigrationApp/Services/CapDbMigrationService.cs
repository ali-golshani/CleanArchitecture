using DotNetCore.CAP.Persistence;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

public class CapDbMigrationService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public void Migrate()
    {
        Console.WriteLine("CapSql ...");
        Service<IStorageInitializer>().InitializeAsync(default).Wait();
        Console.WriteLine("Migration Finished .");
    }
}
