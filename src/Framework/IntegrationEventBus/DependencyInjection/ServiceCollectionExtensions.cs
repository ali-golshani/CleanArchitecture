using System.Text.Json;
using IntegrationEventBus;
using IntegrationEventBus.Internal;
using IntegrationEventBus.Processing;
using IntegrationEventBus.Serialization;
using IntegrationEventBus.Topology;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

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
