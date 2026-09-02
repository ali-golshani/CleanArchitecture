using System.Text.Json;
using IntegrationEventBus.Core.Topology;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationEventBus.Core;

/// <summary>
/// Provides provider and hosting extensions with access to the configured service collection.
/// </summary>
public sealed class IntegrationEventBusBuilder
{
    internal IntegrationEventBusBuilder(
        IServiceCollection services,
        IntegrationEventTopology topology,
        JsonSerializerOptions serializerOptions)
    {
        Services = services;
        Topology = topology;
        SerializerOptions = serializerOptions;
    }

    public IServiceCollection Services { get; }
    public IntegrationEventTopology Topology { get; }
    public JsonSerializerOptions SerializerOptions { get; }
}
