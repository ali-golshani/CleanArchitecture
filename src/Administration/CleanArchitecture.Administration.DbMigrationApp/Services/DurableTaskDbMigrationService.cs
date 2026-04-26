using DurableTask.Core;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class DurableTaskDbMigrationService(IServiceProvider serviceProvider)
{
    public async Task Migrate()
    {
        Console.WriteLine("Migrate DurableTaskSql ...");
        var worker = serviceProvider.GetRequiredService<TaskHubWorker>();
        await worker.orchestrationService.CreateIfNotExistsAsync();
        Console.WriteLine("Migration Finished .");
    }
}
