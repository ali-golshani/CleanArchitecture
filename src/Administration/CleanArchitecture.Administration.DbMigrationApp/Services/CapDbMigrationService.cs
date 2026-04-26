using DotNetCore.CAP.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class CapDbMigrationService(IServiceProvider serviceProvider)
{
    public async Task Migrate()
    {
        Console.WriteLine("Migrate CapSql ...");
        await serviceProvider.GetRequiredService<IStorageInitializer>().InitializeAsync(default);
        Console.WriteLine("Migration Finished .");
    }
}
