using Framework.Persistence;

using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Framework.Messaging.MassTransit;

internal sealed class MassTransitOutboxTransaction(
    DbTransaction transaction,
    IDbContextTransaction massTransitTransaction) : IOutboxTransaction
{
    private readonly DbTransaction transaction = transaction;
    private readonly IDbContextTransaction massTransitTransaction = massTransitTransaction;

    public DbTransaction DbTransaction => transaction;

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
        try
        {
            await massTransitTransaction.DisposeAsync();
        }
        finally
        {
            await transaction.DisposeAsync();
        }
    }
}
