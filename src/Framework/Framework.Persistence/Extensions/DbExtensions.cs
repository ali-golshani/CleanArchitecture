using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence.Extensions;

public static class DbExtensions
{
    public static Microsoft.Data.SqlClient.SqlConnection SqlConnection(this DbContext db)
    {
        return new Microsoft.Data.SqlClient.SqlConnection(db.Database.GetConnectionString());
    }
}
