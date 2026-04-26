using CleanArchitecture.Administration.HostedDebugApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.Administration.HostedDebugApp;

internal class ApplicationHostedService(IServiceScopeFactory serviceScopeFactory) : IHostedService
{
    private readonly IServiceScopeFactory serviceScopeFactory = serviceScopeFactory;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceScopeFactory.CreateScope();
        await scope.ServiceProvider.GetRequiredService<RegisterAndApproveOrderSchedulingService>().ScheduleNextOrder();
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}