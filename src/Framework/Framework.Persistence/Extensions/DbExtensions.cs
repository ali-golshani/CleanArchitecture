using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Framework.Persistence.Extensions;

public static class DbExtensions
{
    public static Microsoft.Data.SqlClient.SqlConnection SqlConnection(this DbContext db)
    {
        return new Microsoft.Data.SqlClient.SqlConnection(db.Database.GetConnectionString());
    }

    public static void ConfigureMigrationsHistoryTable(this SqlServerDbContextOptionsBuilder options)
    {
        options.MigrationsHistoryTable("DbMigrationsHistory", "dbo");
    }
}
