using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class SqlEventBusDbMigrationService(IServiceProvider serviceProvider)
{
    public async Task Migrate()
    {
        Console.WriteLine("Migrate SqlEventBus ...");
        await serviceProvider.GetRequiredService<IntegrationEventBus.EventBusMigrator>().MigrateAsync();
        Console.WriteLine("Migration Finished .");
    }
}
