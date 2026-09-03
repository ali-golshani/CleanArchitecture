using IntegrationEventBus.Sql;
using Microsoft.Data.SqlClient;

namespace IntegrationEventBus;

/// <summary>
/// Creates the SQL Server objects required by the integration event bus.
/// </summary>
public sealed class EventBusMigrator
{
    private readonly EventBusOptions _options;

    internal EventBusMigrator(EventBusOptions options)
    {
        _options = options;
    }

    /// <summary>
    /// Creates any missing schema and tables required by the integration event bus.
    /// </summary>
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = new SqlConnection(_options.ConnectionString);
        await connection.OpenAsync(cancellationToken).ConfigureAwait(false);
        await using var command = connection.CreateCommand();
        command.CommandTimeout = _options.CommandTimeoutSeconds;
        await SqlQueries.CreateSchemaAsync(command, cancellationToken);
    }
}
