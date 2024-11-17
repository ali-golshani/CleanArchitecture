namespace Framework.Threading.BackgroundServices;

public sealed class EmptyDistributedLock : IDistributedLock
{
    public IDisposable? TryAcquire(CancellationToken cancellationToken)
    {
        return new Empty();
    }

    private sealed class Empty : IDisposable
    {
        public void Dispose() { }
    }
}
