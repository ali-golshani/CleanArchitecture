using Microsoft.Extensions.Hosting;

namespace Infrastructure.RequestAudit;

public class RequestAuditBackgroundService : BackgroundService
{
    private readonly RequestAuditAgent commandAuditAgent;

    public RequestAuditBackgroundService(RequestAuditAgent commandAuditAgent)
    {
        this.commandAuditAgent = commandAuditAgent;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await commandAuditAgent.EnsureStarted(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            await commandAuditAgent.EnsureStarted(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }
}
