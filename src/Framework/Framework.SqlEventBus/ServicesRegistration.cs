using IntegrationEventBus.Core.DependencyInjection;
using IntegrationEventBus.Core.Topology;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Framework.SqlEventBus;

public static class ServicesRegistration
{
    public static void AddSqlMessaging(
        this IServiceCollection services,
        Action<IntegrationEventTopologyBuilder> configureTopology,
        Action<JsonSerializerOptions>? configureSerialization = null)
    {
        services.AddIntegrationEventBus(configureTopology, configureSerialization);

        ServicesConfiguration.RegisterSqlEventOutbox(services);
    }
}
