using IntegrationEventBus.SqlServer.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class SqlEventBusDbMigrationService(IServiceProvider serviceProvider)
{
    public async Task Migrate()
    {
        Console.WriteLine("Migrate SqlEventBus ...");
        await serviceProvider.GetRequiredService<SqlServerIntegrationEventBusMigrator>().MigrateAsync();
        Console.WriteLine("Migration Finished .");
    }
}
