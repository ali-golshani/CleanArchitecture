using Framework.Application;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.Cap;

internal sealed class OutboxTransaction(IDbContextTransaction transaction) : IOutboxTransaction
{
    private readonly IDbContextTransaction transaction = transaction;

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
