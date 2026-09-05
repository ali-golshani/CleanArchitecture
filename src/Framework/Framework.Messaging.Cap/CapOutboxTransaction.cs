using Framework.Messaging;

using DotNetCore.CAP;
using System.Data.Common;

namespace Framework.Messaging.Cap;

internal sealed class CapOutboxTransaction(DbTransaction transaction) : IOutboxTransaction
{
    private readonly DbTransaction transaction = transaction;

    public DbTransaction DbTransaction => transaction;

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync()
    {
        await transaction.RollbackAsync(CancellationToken.None);
    }

    public async ValueTask DisposeAsync()
    {
        await transaction.DisposeAsync();
    }
}