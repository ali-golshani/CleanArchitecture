using DurableTask.Core;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal sealed class DurableTaskDbMigrationService(IServiceProvider serviceProvider) : DbMigrationServiceBase(serviceProvider)
{
    public void Migrate()
    {
        Console.WriteLine("DurableTaskSql ...");
        var worker = Service<TaskHubWorker>();
        worker.orchestrationService.CreateIfNotExistsAsync().Wait();
        Console.WriteLine("Migration Finished .");
    }
}
