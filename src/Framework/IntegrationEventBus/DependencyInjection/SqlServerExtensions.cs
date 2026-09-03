using IntegrationEventBus;
using IntegrationEventBus.Internal;
using IntegrationEventBus.Processing;
using IntegrationEventBus.Publishing;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class SqlServerExtensions
{
    public static IntegrationEventBusBuilder UseSqlServer(
        this IntegrationEventBusBuilder builder,
        string connectionString,
        Action<EventBusOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        var options = new EventBusOptions
        {
            ConnectionString = connectionString
        };
        configure?.Invoke(options);
        options.Validate();

        builder.Services.AddSingleton(options);
        builder.Services.TryAddSingleton(static provider =>
        {
            return new EventBusMigrator(provider.GetRequiredService<EventBusOptions>());
        });
        builder.Services.TryAddSingleton<IIntegrationEventPublisher, EventPublisher>();
        builder.Services.TryAddSingleton<ISubscriptionRunner, SubscriptionRunner>();

        return builder;
    }
}
