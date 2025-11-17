using CleanArchitecture.Administration.DbMigrationApp.Services;
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
            var auditDb = new AuditDbMigrationService(serviceProvider);
            var orderingDb = new OrderingDbMigrationService(serviceProvider);
            var massTransitDb = new MassTransitDbMigrationService(serviceProvider);
            var capDb = new CapDbMigrationService(serviceProvider);
            var durableTaskDb = new DurableTaskDbMigrationService(serviceProvider);

            auditDb.Migrate();

            Console.WriteLine();

            orderingDb.Migrate();

            Console.WriteLine();

            massTransitDb.Migrate();
            Console.WriteLine();
            massTransitDb.MigrateSqlTransport();

            Console.WriteLine();

            capDb.Migrate();

            Console.WriteLine();

            durableTaskDb.Migrate();
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
