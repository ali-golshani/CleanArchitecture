using DotNetCore.CAP.Persistence;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class CapDbMigrationService(IServiceProvider serviceProvider) : DbMigrationServiceBase(serviceProvider)
{
    public void Migrate()
    {
        Console.WriteLine("CapSql ...");
        Service<IStorageInitializer>().InitializeAsync(default).Wait();
        Console.WriteLine("Migration Finished .");
    }
}
