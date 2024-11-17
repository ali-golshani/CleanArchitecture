using DotNetCore.CAP.Persistence;

namespace CleanArchitecture.Administration.DbMigrationApp;

public class CapMigrateDbService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public void CapSql()
    {
        Console.WriteLine("CapSql ...");
        Service<IStorageInitializer>().InitializeAsync(default).Wait();
        Console.WriteLine("Migration Finished .");
    }
}
