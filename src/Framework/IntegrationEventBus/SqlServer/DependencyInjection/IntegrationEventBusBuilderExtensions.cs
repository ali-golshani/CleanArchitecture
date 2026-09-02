using IntegrationEventBus.Abstractions;
using IntegrationEventBus.Core;
using IntegrationEventBus.Core.Infrastructure;
using IntegrationEventBus.SqlServer.Processing;
using IntegrationEventBus.SqlServer.Publishing;
using IntegrationEventBus.SqlServer.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationEventBus.SqlServer.DependencyInjection;

public static class SqlServerIntegrationEventBusBuilderExtensions
{
    public static IntegrationEventBusBuilder UseSqlServer(
        this IntegrationEventBusBuilder builder,
        string connectionString,
        Action<SqlServerIntegrationEventBusOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(builder);
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        var options = new SqlServerIntegrationEventBusOptions
        {
            ConnectionString = connectionString
        };
        configure?.Invoke(options);
        options.Validate();

        builder.Services.AddSingleton(options);
        builder.Services.TryAddSingleton(static provider =>
        {
            return new SqlServerIntegrationEventBusMigrator(provider.GetRequiredService<SqlServerIntegrationEventBusOptions>());
        });
        builder.Services.TryAddSingleton<IIntegrationEventPublisher, SqlServerIntegrationEventPublisher>();
        builder.Services.TryAddSingleton<ISubscriptionRunner, SqlServerSubscriptionRunner>();

        return builder;
    }
}
