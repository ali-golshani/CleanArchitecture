using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp.Services;

internal abstract class DbMigrationServiceBase(IServiceProvider serviceProvider)
{
    protected readonly IServiceProvider serviceProvider = serviceProvider;

    protected T Service<T>() where T : notnull => serviceProvider.GetRequiredService<T>();

    protected static void PrintMigrationInfo(DatabaseFacade database)
    {
        PrintConnectionString(database);
        PrintPendingMigrations(database);
    }

    protected static void PrintPendingMigrations(DatabaseFacade database)
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

    protected static void PrintConnectionString(DatabaseFacade database)
    {
        var cs = database.GetConnectionString();
        Console.WriteLine($"ConnectionString.Contains(Server=.) = {cs?.Contains("Server=.")}");
    }
}
