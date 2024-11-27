using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Savorboard.CAP.InMemoryMessageQueue;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class CapConfigs
{
    public static void RegisterCap(
        IServiceCollection services,
        IConfiguration configuration,
        string connectionString)
    {
        var options = CapOptions.From(configuration);

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

    public class CapOptions
    {
        public static readonly CapOptions Default = new CapOptions();

        public int FailedRetryCount { get; set; } = 5;
        public int FailedRetryInterval { get; set; } = 5 * 60;
        public int ConsumerThreadCount { get; set; } = 1;

        public static CapOptions From(IConfiguration configuration)
        {
            var section = configuration.GetSection(Configurations.ConfigurationSections.Cap.Options);
            var options = section.Get<CapOptions>();
            return options ?? Default;
        }
    }
}