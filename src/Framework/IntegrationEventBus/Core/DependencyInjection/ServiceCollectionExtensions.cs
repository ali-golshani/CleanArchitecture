using System.Text.Json;
using IntegrationEventBus.Core.Infrastructure;
using IntegrationEventBus.Core.Processing;
using IntegrationEventBus.Core.Serialization;
using IntegrationEventBus.Core.Topology;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationEventBus.Core.DependencyInjection;

public static class IntegrationEventBusServiceCollectionExtensions
{
    /// <summary>
    /// Registers the IntegrationEventBus core and its explicit topology.
    /// </summary>
    public static IntegrationEventBusBuilder AddIntegrationEventBus(
        this IServiceCollection services,
        Action<IntegrationEventTopologyBuilder> configureTopology,
        Action<JsonSerializerOptions>? configureSerialization = null)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configureTopology);

        var topologyBuilder = new IntegrationEventTopologyBuilder();
        configureTopology(topologyBuilder);
        var topology = topologyBuilder.Build();

        var serializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        configureSerialization?.Invoke(serializerOptions);

        services.TryAddSingleton(topology);
        services.TryAddSingleton(serializerOptions);
        services.TryAddSingleton<IIntegrationEventSerializer, SystemTextJsonIntegrationEventSerializer>();
        services.TryAddSingleton<IIntegrationEventDispatcher, IntegrationEventDispatcher>();
        services.TryAddSingleton<IProcessorSignal, ProcessorSignal>();

        foreach (var handlerType in topology.Subscriptions
                     .SelectMany(static subscription => subscription.Routes.Values)
                     .OfType<Type>()
                     .Distinct())
        {
            services.TryAdd(ServiceDescriptor.Scoped(handlerType, handlerType));
        }

        return new IntegrationEventBusBuilder(services, topology, serializerOptions);
    }
}
