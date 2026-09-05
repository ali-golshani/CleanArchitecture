using Framework.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence;

public static class DualDbContext
{
    public static async Task<DualDbContextTransaction> BeginTransaction(
        DbContext firstDb,
        DbContext secondDb,
        CancellationToken cancellationToken)
    {
        var connection = firstDb.SqlConnection();
        await connection.OpenAsync(cancellationToken);

        firstDb.Database.SetDbConnection(connection);
        secondDb.Database.SetDbConnection(connection);

        var transaction = await connection.BeginTransactionAsync(cancellationToken);

        var firstDbTransaction = (await firstDb.Database.UseTransactionAsync(transaction, cancellationToken))!;
        var secondDbTransaction = (await secondDb.Database.UseTransactionAsync(transaction, cancellationToken))!;

        return new DualDbContextTransaction(
            connection,
            transaction,
            firstDbTransaction,
            secondDbTransaction);
    }
}
