using Framework.Application;
using System.Data.Common;

namespace Framework.IntegrationEventBus;

internal sealed class OutboxTransaction(DbTransaction transaction) : IOutboxTransaction
{
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
    }
}
