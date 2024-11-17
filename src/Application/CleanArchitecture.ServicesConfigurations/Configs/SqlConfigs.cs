using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.ServicesConfigurations.Configs;

internal static class SqlConfigs
{
    public static void Configure(
        DbContextOptionsBuilder optionsBuilder,
        string connectionString,
        string migrationsSchema)
    {
        optionsBuilder.UseSqlServer(
                connectionString,
                options =>
                {
                    options.MigrationsHistoryTable("DbMigrationsHistory", migrationsSchema);
                });
    }
}