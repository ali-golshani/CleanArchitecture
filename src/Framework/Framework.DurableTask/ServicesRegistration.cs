using Microsoft.Extensions.DependencyInjection;

namespace Framework.DurableTask;

public static class ServicesRegistration
{
    public static void RegisterDurableTask(
        this IServiceCollection services,
        string taskHubname,
        string dbConnectionString,
        DurableTaskOptions durableTaskOptions,
        IOrchestrationsRegistrar orchestrationsRegistrar)
    {
        var sqlOptions = new SqlOptions { ConnectionString = dbConnectionString };

        ServicesConfiguration.RegisterOrchestrationService(services, sqlOptions, durableTaskOptions, taskHubname);
        ServicesConfiguration.RegisterTaskHubWorker(services, orchestrationsRegistrar);
        ServicesConfiguration.RegisterTaskHubClient(services);
        ServicesConfiguration.RegisterHostedServices(services);
    }
}