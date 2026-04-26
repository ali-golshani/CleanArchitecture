namespace Framework.Threading.BackgroundServices;

public interface IDistributedLock
{
    IDisposable? TryAcquire(CancellationToken cancellationToken);
}
