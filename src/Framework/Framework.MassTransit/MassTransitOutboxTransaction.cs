using Framework.Application;
using System.Data.Common;

namespace Framework.MassTransit;

internal sealed class MassTransitOutboxTransaction(DbConnection connection, DbTransaction transaction) : IOutboxTransaction
{
    private readonly DbConnection connection = connection;
    private readonly DbTransaction transaction = transaction;

    public async Task CommitAsync()
    {
        await transaction.CommitAsync();
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
