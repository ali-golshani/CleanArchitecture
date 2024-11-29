using CleanArchitecture.Actors;
using CleanArchitecture.Administration.DebugApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DebugApp;

internal static class Program
{
    static async Task Main()
    {
        var services = ServiceCollectionBuilder.Build(out _);
        var rootServiceProvider = services.BuildServiceProvider();

        BackgroundServices.Start(rootServiceProvider);

        using var scope = rootServiceProvider.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        await serviceProvider.GetRequiredService<RegisterAndApproveOrderService>().Run();

        await BackgroundServices.Stop();

        Exit();
    }

    private static void Exit()
    {
        Console.Write("Press Ctrl + C to exit ...");
    }
}
