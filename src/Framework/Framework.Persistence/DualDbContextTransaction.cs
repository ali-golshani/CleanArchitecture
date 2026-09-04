using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Framework.Persistence;

public sealed class DualDbContextTransaction
{
    internal DualDbContextTransaction(
        DbConnection connection,
        DbTransaction transaction,
        IDbContextTransaction firstDbTransaction,
        IDbContextTransaction secondDbTransaction)
    {
        Connection = connection;
        Transaction = transaction;
        FirstDbTransaction = firstDbTransaction;
        SecondDbTransaction = secondDbTransaction;
    }

    public DbConnection Connection { get; }
    public DbTransaction Transaction { get; }
    public IDbContextTransaction FirstDbTransaction { get; }
    public IDbContextTransaction SecondDbTransaction { get; }
}
