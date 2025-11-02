using Framework.Cap;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Framework.Cap.InMemoryTransport;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class CapConfigs
{
    public static void RegisterCap(
        IServiceCollection services,
        IConfiguration configuration,
        string connectionString)
    {
        var options = CapOptions(configuration);

        services.AddCap(
            x =>
            {
                x.UseSqlServer(connectionString);
                x.UseInMemoryMessageQueue();
                x.UseDashboard();
                x.FailedRetryCount = options.FailedRetryCount;
                x.FailedRetryInterval = options.FailedRetryInterval;
                x.ConsumerThreadCount = options.ConsumerThreadCount;
            });
    }

    public static CapOptions CapOptions(IConfiguration configuration)
    {
        var section = configuration.GetSection(Configurations.ConfigurationSections.Cap.Options);
        var options = section.Get<CapOptions>();
        return options ?? Framework.Cap.CapOptions.Default;
    }
}