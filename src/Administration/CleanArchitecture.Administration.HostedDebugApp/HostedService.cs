using CleanArchitecture.Administration.HostedApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Administration.HostedApp;

internal class HostedService(IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    private readonly IServiceScopeFactory serviceScopeFactory = serviceScopeFactory;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        using var scope = serviceScopeFactory.CreateScope();
        var app = scope.ServiceProvider.GetRequiredService<RegisterOrderService>();
        await app.Run();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}