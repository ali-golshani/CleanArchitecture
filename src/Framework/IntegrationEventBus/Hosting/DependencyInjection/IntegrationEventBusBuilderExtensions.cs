using IntegrationEventBus.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace IntegrationEventBus.Hosting.DependencyInjection;

public static class HostingIntegrationEventBusBuilderExtensions
{
    /// <summary>
    /// Adds the background processor that runs one serial loop for each local subscription.
    /// </summary>
    public static IntegrationEventBusBuilder AddHostedProcessor(this IntegrationEventBusBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IHostedService, IntegrationEventBusHostedService>());
        return builder;
    }
}
