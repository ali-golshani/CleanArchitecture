using Framework.Messaging;
using Framework.Persistence;

namespace Framework.Messaging.MassTransit;

internal sealed class MassTransitOutboxTransaction(DualDbContextTransaction transaction) : IOutboxTransaction
{
    private readonly DualDbContextTransaction transaction = transaction;

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await transaction.Transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackAsync()
    {
        await transaction.Transaction.RollbackAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await transaction.SecondDbTransaction.DisposeAsync();
        await transaction.FirstDbTransaction.DisposeAsync();
        await transaction.Transaction.DisposeAsync();
        await transaction.Connection.DisposeAsync();
    }
}
