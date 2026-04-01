using DurableTask.Core;
using Framework.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class DurableTaskConfigs
{
    private const string TaskHubname = "CleanArchitectureTaskHub";

    public static void RegisterDurableTask(
        IServiceCollection services,
        IConfiguration configuration,
        string connectionString)
    {
        var options = DurableTaskOptions(configuration);
        var sqlOptions = new SqlOptions { ConnectionString = connectionString };

        ServicesConfiguration.RegisterOrchestrationService(services, sqlOptions, options, TaskHubname);
        ServicesConfiguration.RegisterTaskHubWorker(services, new OrchestrationsRegistrar());
        ServicesConfiguration.RegisterTaskHubClient(services);
        ServicesConfiguration.RegisterHostedServices(services);
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
            ProcessManager.ServicesConfiguration.RegisterOrchestrations(serviceProvider, worker);
        }
    }
}