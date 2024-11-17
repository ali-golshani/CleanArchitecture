using Framework.Application;
using System.Data.Common;

namespace Framework.MassTransit;

internal sealed class OutboxTransaction(DbConnection connection, DbTransaction transaction) : IOutboxTransaction
{
    private readonly DbConnection connection = connection;
    private readonly DbTransaction transaction = transaction;

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
        connection.Dispose();
    }
}
