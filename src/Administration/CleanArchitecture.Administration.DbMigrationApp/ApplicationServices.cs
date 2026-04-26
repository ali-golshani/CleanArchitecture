using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DbMigrationApp;

internal static class ApplicationServices
{
    private static readonly IServiceProvider rootServiceProvider = BuildRootServiceProvider();

    public static IServiceScope CreateScope()
    {
        return rootServiceProvider.CreateScope();
    }

    private static ServiceProvider BuildRootServiceProvider()
    {
        var services = ServiceCollectionBuilder.Build(out _);
        return services.BuildServiceProvider();
    }
}
