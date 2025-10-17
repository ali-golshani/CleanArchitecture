using Framework.Application;
using Microsoft.EntityFrameworkCore.Storage;

namespace Framework.Cap;

internal sealed class OutboxTransaction(IDbContextTransaction transaction) : IOutboxTransaction
{
    private readonly IDbContextTransaction transaction = transaction;

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
