using CleanArchitecture.Administration.DebugApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DebugApp;

internal static class Program
{
    static async Task Main()
    {
        using (var scope = ApplicationServices.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            await Run(serviceProvider);
        }

        await ApplicationServices.FlushAuditAsync();

        Console.Write("Press Ctrl + C to exit ...");
    }

    private static async Task Run(IServiceProvider serviceProvider)
    {
        await serviceProvider.GetRequiredService<RegisterOrderService>().RegisterOrder();
    }
}
