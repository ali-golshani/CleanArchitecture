namespace CleanArchitecture.Administration.DbMigrationApp;

internal static class Program
{
    public static void Main()
    {
        Console.WriteLine();
    }

    public static void MigrateAll()
    {
        try
        {
            var migrateDbApp = new MigrateDbApp();
            var massTransitMigrateDbApp = new MassTransitMigrateDbApp();
            var capMigrateDbApp = new CapMigrateDbApp();

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

        Console.WriteLine();
        Console.WriteLine("Press Enter to Exit ...");
    }
}
