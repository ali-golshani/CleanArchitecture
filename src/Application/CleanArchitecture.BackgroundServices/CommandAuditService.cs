using CleanArchitecture.Audit;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.BackgroundServices;

public class CommandAuditService : BackgroundService
{
    private readonly CommandAuditAgent commandAuditAgent;

    public CommandAuditService(CommandAuditAgent commandAuditAgent)
    {
        this.commandAuditAgent = commandAuditAgent;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        commandAuditAgent.Start(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            commandAuditAgent.EnsureStarted();
            await Task.Delay(Settings.EnsureStartedTimeout, stoppingToken);
        }
    }
}
