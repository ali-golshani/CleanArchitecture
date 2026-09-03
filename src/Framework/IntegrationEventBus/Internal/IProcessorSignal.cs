namespace IntegrationEventBus.Internal;

internal interface IProcessorSignal
{
    void Pulse();
    Task WaitAsync(TimeSpan timeout, CancellationToken cancellationToken);
}
