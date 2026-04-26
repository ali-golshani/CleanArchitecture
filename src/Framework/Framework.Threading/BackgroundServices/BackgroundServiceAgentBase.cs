namespace Framework.Threading.BackgroundServices;

public abstract class BackgroundServiceAgentBase
{
    protected abstract Task Executing(CancellationToken cancellationToken);
    protected abstract IDistributedLock DistributedLock();
    protected abstract bool IsEnable { get; }

    private int evaluatingThreadStatus = 0;

    public async Task EnsureStarted(CancellationToken cancellationToken)
    {
        if (!IsEnable || cancellationToken.IsCancellationRequested)
        {
            return;
        }

        if (Interlocked.Exchange(ref evaluatingThreadStatus, 1) == 0)
        {
            try
            {
                await TryExecuting(cancellationToken);
            }
            finally
            {
                Interlocked.Exchange(ref evaluatingThreadStatus, 0);
            }
        }
    }

    private async Task TryExecuting(CancellationToken cancellationToken)
    {
        IDisposable? handle = null;

        try
        {
            var @lock = DistributedLock();
            handle = @lock.TryAcquire(cancellationToken);

            if (handle == null)
            {
                await OnAcquiringDistributedLockFailure();
                return;
            }

            await Executing(cancellationToken);
        }
        finally
        {
            handle?.Dispose();
        }
    }

    protected virtual Task OnAcquiringDistributedLockFailure()
    {
        return Task.CompletedTask;
    }
}
