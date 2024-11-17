using Framework.Application;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.Cap;

internal sealed class OutboxTransaction : IOutboxTransaction
{
    private readonly IDbContextTransaction transaction;

    public OutboxTransaction(IDbContextTransaction transaction)
    {
        this.transaction = transaction;
    }

    public void Commit()
    {
        transaction.Commit();
    }

    public void Rollback()
    {
        transaction.Rollback();
    }

    public void Dispose()
    {
        transaction.Dispose();
    }
}
