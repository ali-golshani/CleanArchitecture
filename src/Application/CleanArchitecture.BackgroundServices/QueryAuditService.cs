using CleanArchitecture.Audit;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.BackgroundServices;

public class QueryAuditService : BackgroundService
{
    private readonly QueryAuditAgent queryAuditAgent;

    public QueryAuditService(QueryAuditAgent queryAuditAgent)
    {
        this.queryAuditAgent = queryAuditAgent;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        queryAuditAgent.Start(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            queryAuditAgent.EnsureStarted();
            await Task.Delay(Settings.EnsureStartedTimeout, stoppingToken);
        }
    }
}
