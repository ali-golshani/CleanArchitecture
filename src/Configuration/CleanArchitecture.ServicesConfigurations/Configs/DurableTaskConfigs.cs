using DurableTask.Core;
using Framework.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class DurableTaskConfigs
{
    public static void RegisterDurableTask(
        IServiceCollection services,
        IConfiguration configuration,
        string connectionString)
    {
        var options = DurableTaskOptions(configuration);
        var sqlOptions = new SqlOptions { ConnectionString = connectionString };

        ServiceConfigurations.RegisterOrchestrationService(services, sqlOptions, options);
        ServiceConfigurations.RegisterTaskHubWorker(services, new OrchestrationsRegistrar());
        ServiceConfigurations.RegisterTaskHubClient(services);
        ServiceConfigurations.RegisterHostedServices(services);
    }

    private static DurableTaskOptions DurableTaskOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection(Configurations.ConfigurationSections.DurableTask.Options);
        var options = section.Get<DurableTaskOptions>();
        return options ?? new();
    }

    private sealed class OrchestrationsRegistrar : IOrchestrationsRegistrar
    {
        public void RegisterOrchestrations(IServiceProvider serviceProvider, TaskHubWorker worker)
        {
            ProcessManager.ServiceConfigurations.RegisterOrchestrations(serviceProvider, worker);
        }
    }
}