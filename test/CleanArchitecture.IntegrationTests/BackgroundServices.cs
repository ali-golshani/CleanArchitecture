using Infrastructure.RequestAudit;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests;

internal static class BackgroundServices
{
    private static IServiceScope serviceScope = null!;
    private static CancellationTokenSource cts = null!;

    public static void Start(IServiceProvider rootServiceProvider)
    {
        serviceScope = rootServiceProvider.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        cts = new CancellationTokenSource();
        var auditAgent = serviceProvider.GetRequiredService<RequestAuditAgent>();
        auditAgent.Start(cts.Token);
    }

    public static async Task Stop()
    {
        await Task.Delay(1000);
        cts.Cancel();
        serviceScope.Dispose();
    }
}
