using Framework.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Framework.Persistence;

public static class DualDbContext
{
    public static async Task<(DbConnection Connection, DbTransaction Transaction)> BeginTransaction(DbContext firstDb, DbContext secondDb, CancellationToken cancellationToken)
    {
        var connection = firstDb.SqlConnection();
        await connection.OpenAsync(cancellationToken);

        firstDb.Database.SetDbConnection(connection);
        secondDb.Database.SetDbConnection(connection);

        var transaction = await connection.BeginTransactionAsync(cancellationToken);

        await firstDb.Database.UseTransactionAsync(transaction, cancellationToken);
        await secondDb.Database.UseTransactionAsync(transaction, cancellationToken);

        return new (connection, transaction);
    }
}
