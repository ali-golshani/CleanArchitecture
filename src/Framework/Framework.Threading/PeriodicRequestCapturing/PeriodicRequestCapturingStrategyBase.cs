namespace Framework.Threading.PeriodicRequestCapturing;

public abstract class PeriodicRequestCapturingStrategyBase : IDisposable
{
    protected abstract void Evaluate();
    protected abstract void Disposing();

    private bool disposed;
    private volatile bool signalled;
    private DateTime lastEvaluationTime;

    protected PeriodicRequestCapturingStrategyBase(
        TimeSpan minIntervalTime,
        TimeSpan? maxIntervalTime = null)
    {
        MinIntervalTime = minIntervalTime;
        MaxIntervalTime = maxIntervalTime ?? TimeSpan.MaxValue;

        disposed = false;
        signalled = false;
        lastEvaluationTime = Time;
    }

    public TimeSpan MinIntervalTime { get; }
    public TimeSpan MaxIntervalTime { get; }

    public void Signal()
    {
        signalled = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposed)
        {
            return;
        }

        if (disposing)
        {
            Disposing();
        }

        disposed = true;
    }

    private static DateTime Time => DateTime.Now;

    protected void OnTimer()
    {
        var elapsed = Time - lastEvaluationTime;

        if (elapsed >= MaxIntervalTime || elapsed >= MinIntervalTime && signalled)
        {
            Evaluating();
        }
    }

    private void Evaluating()
    {
        signalled = false;
        lastEvaluationTime = Time;
        Evaluate();
    }
}