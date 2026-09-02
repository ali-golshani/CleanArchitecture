using Framework.Application;
using System.Data.Common;

namespace Framework.SqlEventBus;

internal sealed class SqlOutboxTransaction(DbTransaction transaction) : IOutboxTransaction
{
    private readonly DbTransaction transaction = transaction;

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync()
    {
        await transaction.RollbackAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await transaction.DisposeAsync();
    }
}
