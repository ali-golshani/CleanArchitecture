using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Administration.DbMigrationApp.DesignTimeDbContext;

internal static class SqlConfigs
{
    private static string ConnectionString { get; } = Properties.Resources.DotnetEfMigrationsConnectionString;

    public static void Configure(DbContextOptionsBuilder optionsBuilder, string migrationsSchema)
    {
        optionsBuilder.UseSqlServer(
            ConnectionString,
            options =>
            {
                options.MigrationsHistoryTable("DbMigrationsHistory", migrationsSchema);
            });
    }
}