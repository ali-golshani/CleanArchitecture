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

        using var scope = rootServiceProvider.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        await serviceProvider.GetRequiredService<RegisterOrderCommandService>().Run();

        Exit();
    }

    private static void Exit()
    {
        Console.WriteLine("Please Wait...");
        Thread.Sleep(1000);
        Console.Write("Press Ctrl + C to exit ...");
    }
}
