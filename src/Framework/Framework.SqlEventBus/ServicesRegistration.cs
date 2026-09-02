using IntegrationEventBus.Core.DependencyInjection;
using IntegrationEventBus.Core.Topology;
using IntegrationEventBus.Hosting.DependencyInjection;
using IntegrationEventBus.SqlServer.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Framework.SqlEventBus;

public static class ServicesRegistration
{
    public static void AddSqlMessaging(
        this IServiceCollection services,
        string dbConnectionString,
        Action<IntegrationEventTopologyBuilder> configureTopology,
        Action<JsonSerializerOptions>? configureSerialization = null)
    {
        services
            .AddIntegrationEventBus(configureTopology, configureSerialization)
            .UseSqlServer(dbConnectionString)
            .AddHostedProcessor();

        ServicesConfiguration.RegisterSqlEventOutbox(services);
    }
}
