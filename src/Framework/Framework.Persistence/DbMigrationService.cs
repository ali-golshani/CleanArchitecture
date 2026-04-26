using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Persistence;

public abstract class DbMigrationService<TContext>(IServiceProvider serviceProvider)
    where TContext : DbContext
{
    protected readonly IServiceProvider serviceProvider = serviceProvider;

    public async Task Migrate()
    {
        Console.WriteLine($"Migrate {typeof(TContext).Name} ...");

        using (var db = serviceProvider.GetRequiredService<TContext>())
        {
            PrintMigrationInfo(db.Database);
            await db.Database.MigrateAsync();
        }

        Console.WriteLine("Migration Finished .");
    }

    private static void PrintMigrationInfo(DatabaseFacade database)
    {
        PrintConnectionString(database);
        PrintPendingMigrations(database);
    }

    private static void PrintPendingMigrations(DatabaseFacade database)
    {
        var pendingMigrations = database.GetPendingMigrations().ToList();

        if (pendingMigrations.Count > 1)
        {
            Console.WriteLine("PendingMigrations.Count > 1");
        }

        foreach (var item in pendingMigrations)
        {
            Console.WriteLine(item);
        }
    }

    private static void PrintConnectionString(DatabaseFacade database)
    {
        var cs = database.GetConnectionString();
        Console.WriteLine($"ConnectionString.Contains(Server=.) = {cs?.Contains("Server=.")}");
    }
}
