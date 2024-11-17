using Microsoft.Extensions.DependencyInjection;
using CleanArchitecture.Audit.Domain;
using CleanArchitecture.Audit.Persistence;

namespace CleanArchitecture.Audit;

public class QueryAuditAgent : AuditAgent<QueryAuditTrail>
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public QueryAuditAgent(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    protected override bool ShouldLog(QueryAuditTrail logEntry)
    {
        return
            logEntry.ShouldLog == true ||
            !logEntry.IsSuccess ||
            logEntry.ResponseTime >= GlobalSettings.Audit.QueryResponseTimeThreshold ||
            GlobalSettings.Audit.LogSuccessQuery
            ;
    }

    protected override async Task Log(IEnumerable<QueryAuditTrail> logEntries)
    {
        using var scope = serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<AuditDbContext>();
        db.Set<QueryAuditTrail>().AddRange(logEntries);
        await db.SaveChangesAsync();
    }
}
