using Infrastructure.RequestAudit;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests;

internal static class ApplicationServices
{
    private static readonly IServiceProvider rootServiceProvider = BuildRootServiceProvider();

    public static IServiceScope CreateScope()
    {
        return rootServiceProvider.CreateScope();
    }

    public static async Task FlushAuditAsync()
    {
        await rootServiceProvider.GetRequiredService<RequestAuditAgent>().FlushAsync();
    }

    private static ServiceProvider BuildRootServiceProvider()
    {
        var services = ServiceCollectionBuilder.Build(out _);
        return services.BuildServiceProvider();
    }
}
