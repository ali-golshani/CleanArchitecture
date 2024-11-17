using CleanArchitecture.Actors;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp;

public abstract class BasicApp
{
    private static readonly IServiceProvider rootServiceProvider = RootServiceProvider();
    protected readonly IServiceProvider serviceProvider;

    private static ServiceProvider RootServiceProvider()
    {
        var services = ServiceCollectionBuilder.Build(out _);
        var result = services.BuildServiceProvider();
        return result;
    }

    protected BasicApp()
    {
        serviceProvider = rootServiceProvider.CreateScope().ServiceProvider;
    }

    protected T Service<T>() where T : notnull => serviceProvider.GetRequiredService<T>();

    protected static void PrintConnectionString(string? connectionString)
    {
        Console.WriteLine($"ConnectionString.Contains(Server=.) = {connectionString?.Contains("Server=.")}");
    }
}
