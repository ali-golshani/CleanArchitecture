using DurableTask.Core;
using DurableTask.SqlServer;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.DurableTask;

public static class ServiceConfigurations
{
    public static void RegisterOrchestrationService(IServiceCollection services, SqlOptions sqlOptions, DurableTaskOptions options, string taskHubname)
    {
        services.AddSingleton(sp =>
        {
            return OrchestrationServiceFactory.GetSqlOrchestrationService(sqlOptions, options, taskHubname);
        });
    }

    public static void RegisterTaskHubWorker(IServiceCollection services, IOrchestrationsRegistrar orchestrationsRegistrar)
    {
        services.AddSingleton(sp =>
        {
            var service = sp.GetRequiredService<SqlOrchestrationService>();
            var worker = new TaskHubWorker(service);
            orchestrationsRegistrar.RegisterOrchestrations(sp, worker);
            return worker;
        });
    }

    public static void RegisterTaskHubClient(IServiceCollection services)
    {
        services.AddSingleton(sp =>
        {
            var service = sp.GetRequiredService<SqlOrchestrationService>();
            return new TaskHubClient(service);
        });
    }

    public static void RegisterHostedServices(IServiceCollection services)
    {
        services.AddHostedService<TaskHubWorkerBackgroundService>();
    }
}
