using Framework.Messaging;
using System.Data.Common;

namespace Framework.Messaging.MassTransit;

internal sealed class MassTransitOutboxTransaction(DbConnection connection, DbTransaction transaction) : IOutboxTransaction
{
    private readonly DbConnection connection = connection;
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
        await connection.DisposeAsync();
    }
}
