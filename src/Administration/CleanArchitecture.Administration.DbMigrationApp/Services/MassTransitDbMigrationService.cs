using Framework.MassTransit;
using Framework.Persistence;
using MassTransit.SqlTransport;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class MassTransitDbMigrationService(IServiceProvider serviceProvider)
    : DbMigrationService<MassTransitDbContext>(serviceProvider)
{
    public async Task MigrateSqlTransport()
    {
        Console.WriteLine("Migrate MassTransitSqlTransport ...");
        await serviceProvider.GetRequiredService<SqlTransportMigrationHostedService>().StartAsync(default);
        Console.WriteLine("Migration Finished .");
    }
}
