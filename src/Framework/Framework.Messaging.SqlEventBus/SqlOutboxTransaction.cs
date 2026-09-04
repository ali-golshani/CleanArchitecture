using Framework.Messaging;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.Messaging.SqlEventBus;

internal sealed class SqlOutboxTransaction(IDbContextTransaction transaction) : IOutboxTransaction
{
    private readonly IDbContextTransaction transaction = transaction;

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
