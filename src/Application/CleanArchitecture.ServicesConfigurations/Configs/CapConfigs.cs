using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Savorboard.CAP.InMemoryMessageQueue;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class CapConfigs
{
    public static void RegisterCap(IServiceCollection services, string connectionString)
    {
        services.AddCap(
            x =>
            {
                x.UseSqlServer(connectionString);
                x.UseInMemoryMessageQueue();
                x.UseDashboard();
                x.FailedRetryCount = 5;
                x.FailedRetryInterval = 10;
            });
    }
}