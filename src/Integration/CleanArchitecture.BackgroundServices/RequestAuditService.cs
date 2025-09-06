using Infrastructure.RequestAudit;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.BackgroundServices;

public class RequestAuditService : BackgroundService
{
    private readonly RequestAuditAgent commandAuditAgent;

    public RequestAuditService(RequestAuditAgent commandAuditAgent)
    {
        this.commandAuditAgent = commandAuditAgent;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await commandAuditAgent.EnsureStarted(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await commandAuditAgent.EnsureStarted(stoppingToken);
            await Task.Delay(Settings.EnsureStartedTimeout, stoppingToken);
        }
    }
}
