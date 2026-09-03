namespace IntegrationEventBus;

public sealed class EventBusOptions
{
    public TimeSpan PollingInterval { get; set; } = TimeSpan.FromSeconds(5);
    public TimeSpan LockRetryInterval { get; set; } = TimeSpan.FromSeconds(15);
    public int CommandTimeoutSeconds { get; set; } = 30;
    internal string ConnectionString { get; set; } = string.Empty;

    internal void Validate()
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ConnectionString);

        if (PollingInterval <= TimeSpan.Zero)
        {
            throw new InvalidOperationException("Polling interval must be positive.");
        }

        if (LockRetryInterval <= TimeSpan.Zero)
        {
            throw new InvalidOperationException("Lock retry interval must be positive.");
        }

        if (CommandTimeoutSeconds < 1)
        {
            throw new InvalidOperationException("Command timeout must be positive.");
        }
    }
}
