using IntegrationEventBus;
using IntegrationEventBus.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class HostingExtensions
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
