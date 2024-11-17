using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp;

internal static class Program
{
    public static void Main()
    {
        var services = ServiceCollectionBuilder.Build(out _);
        var rootServiceProvider = services.BuildServiceProvider();

        using var scope = rootServiceProvider.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        MigrateAll(serviceProvider);

        Exit();
    }

    public static void MigrateAll(IServiceProvider serviceProvider)
    {
        try
        {
            var migrateDbApp = new MigrateCleanDbService(serviceProvider);
            var massTransitMigrateDbApp = new MassTransitMigrateDbService(serviceProvider);
            var capMigrateDbApp = new CapMigrateDbService(serviceProvider);

            migrateDbApp.AuditDbContext();
            Console.WriteLine();
            migrateDbApp.OrderingDbContext();

            Console.WriteLine();

            massTransitMigrateDbApp.MassTransitDbContext();
            Console.WriteLine();
            massTransitMigrateDbApp.MassTransitSqlTransport();

            Console.WriteLine();

            capMigrateDbApp.CapSql();
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp);
        }
    }

    private static void Exit()
    {
        Console.WriteLine("Please Wait...");
        Thread.Sleep(1000);
        Console.Write("Press Ctrl + C to exit ...");
    }
}
