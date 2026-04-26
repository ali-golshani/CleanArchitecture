using CleanArchitecture.Administration.DbMigrationApp.Services;
using CleanArchitecture.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp;

internal static class Program
{
    public static async Task Main()
    {
        using (var scope = ApplicationServices.CreateScope())
        {
            await MigrateAll(scope.ServiceProvider);
        }

        using (var scope = ApplicationServices.CreateScope())
        {
            await SeedAdmin(scope.ServiceProvider);
        }

        Console.Write("Press Ctrl + C to exit ...");
    }

    public static async Task SeedAdmin(IServiceProvider serviceProvider)
    {
        await serviceProvider.GetRequiredService<UserManagement.Persistence.SeedData>().SeedAdmin();
    }

    public static async Task MigrateAll(IServiceProvider serviceProvider)
    {
        try
        {
            var auditDb = new AuditDbMigrationService(serviceProvider);
            await auditDb.Migrate();

            Console.WriteLine();

            var orderingDb = new OrderingDbMigrationService(serviceProvider);
            await orderingDb.Migrate();

            Console.WriteLine();

            var userManagementDb = new UserManagementDbMigrationService(serviceProvider);
            await userManagementDb.Migrate();

            Console.WriteLine();

            var capDb = new CapDbMigrationService(serviceProvider);
            await capDb.Migrate();

            Console.WriteLine();

            var durableTaskDb = new DurableTaskDbMigrationService(serviceProvider);
            await durableTaskDb.Migrate();

            Console.WriteLine();

            if (GlobalSettings.Messaging.SupportMassTransit)
            {
                var massTransitDb = new MassTransitDbMigrationService(serviceProvider);
                await massTransitDb.Migrate();
                Console.WriteLine();
                await massTransitDb.MigrateSqlTransport();
            }
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp);
        }
    }
}
