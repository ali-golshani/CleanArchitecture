using DotNetCore.CAP.Persistence;

namespace CleanArchitecture.Administration.DbMigrationApp;

public class CapMigrateDbApp : BasicApp
{
    public void CapSql()
    {
        Console.WriteLine("CapSql ...");
        Service<IStorageInitializer>().InitializeAsync(default).Wait();
        Console.WriteLine("Migration Finished .");
    }
}
