using CleanArchitecture.Administration.DbMigrationApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp;

internal static class Program
{
    public static async Task Main()
    {
        var services = ServiceCollectionBuilder.Build(out _);
        var rootServiceProvider = services.BuildServiceProvider();

        using (var scope = rootServiceProvider.CreateScope())
        {
            MigrateAll(scope.ServiceProvider);
        }

        using (var scope = rootServiceProvider.CreateScope())
        {
            await SeedAdmin(scope.ServiceProvider);
        }

        await Exit();
    }

    public static async Task SeedAdmin(IServiceProvider serviceProvider)
    {
        await serviceProvider.GetRequiredService<UserManagement.Persistence.SeedData>().SeedAdmin();
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
            var userManagementDb = new UserManagementDbMigrationService(serviceProvider);

            auditDb.Migrate();

            Console.WriteLine();

            orderingDb.Migrate();

            Console.WriteLine();

            userManagementDb.Migrate();

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

    private static async Task Exit()
    {
        Console.WriteLine("Please Wait...");
        await Task.Delay(1000);
        Console.Write("Press Ctrl + C to exit ...");
    }
}
