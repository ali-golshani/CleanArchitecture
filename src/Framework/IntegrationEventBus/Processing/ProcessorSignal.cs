using IntegrationEventBus.Internal;

namespace IntegrationEventBus.Processing;

internal sealed class ProcessorSignal : IProcessorSignal
{
    private readonly Lock _gate = new();
    private TaskCompletionSource _nextPulse = CreateCompletionSource();

    public void Pulse()
    {
        TaskCompletionSource pulse;
        lock (_gate)
        {
            pulse = _nextPulse;
            _nextPulse = CreateCompletionSource();
        }

        pulse.TrySetResult();
    }

    public async Task WaitAsync(TimeSpan timeout, CancellationToken cancellationToken)
    {
        Task pulse;
        lock (_gate)
        {
            pulse = _nextPulse.Task;
        }

        await Task.WhenAny(pulse, Task.Delay(timeout, cancellationToken)).ConfigureAwait(false);
        cancellationToken.ThrowIfCancellationRequested();
    }

    private static TaskCompletionSource CreateCompletionSource()
    {
        return new(TaskCreationOptions.RunContinuationsAsynchronously);
    }
}
