namespace Framework.Core;

public interface IDistributedLock
{
    IDisposable? TryAcquire(CancellationToken cancellationToken);
}
