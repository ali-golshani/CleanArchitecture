using Framework.Threading.BackgroundServices;
using Medallion.Threading.SqlServer;

namespace Framework.Persistence;

internal sealed class SqlDistributedLockWrapper : IDistributedLock
{
    private readonly string dbConnectionString;
    private readonly string distributedLockName;

    public SqlDistributedLockWrapper(string dbConnectionString, string distributedLockName)
    {
        this.dbConnectionString = dbConnectionString;
        this.distributedLockName = distributedLockName;
    }

    public IDisposable? TryAcquire(CancellationToken cancellationToken)
    {
        var @lock = new SqlDistributedLock(distributedLockName, dbConnectionString);
        return @lock.TryAcquire(cancellationToken: cancellationToken);
    }
}
