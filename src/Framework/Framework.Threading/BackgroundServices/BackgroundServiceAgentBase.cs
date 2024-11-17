namespace Framework.Threading.BackgroundServices;

public abstract class BackgroundServiceAgentBase
{
    protected abstract Task Executing(CancellationToken cancellationToken);
    protected abstract IDistributedLock DistributedLock();
    protected abstract bool IsEnable { get; }

    private CancellationToken cancellationToken;
    private int evaluatingThreadStatus = 0;

    public void Start(CancellationToken cancellationToken)
    {
        this.cancellationToken = cancellationToken;
        EnsureStarted();
    }

    public void EnsureStarted()
    {
        if (!IsEnable || cancellationToken.IsCancellationRequested)
        {
            return;
        }

        if (Interlocked.Exchange(ref evaluatingThreadStatus, 1) == 0)
        {
            var thread = new Thread(async () => await TryExecuting(cancellationToken))
            {
                IsBackground = false
            };

            thread.Start();
        }
    }

    private async Task TryExecuting(CancellationToken cancellationToken)
    {
        var @lock = DistributedLock();
        IDisposable? handle = null;

        try
        {
            try
            {
                handle = @lock.TryAcquire(cancellationToken: cancellationToken);

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
        finally
        {
            Interlocked.Exchange(ref evaluatingThreadStatus, 0);
        }
    }

    protected virtual Task OnAcquiringDistributedLockFailure()
    {
        return Task.CompletedTask;
    }

    protected static void Sleep(TimeSpan timeout)
    {
        Thread.Sleep(timeout);
    }

    protected static Task Delay(TimeSpan delay)
    {
        return Task.Delay(delay);
    }
}
