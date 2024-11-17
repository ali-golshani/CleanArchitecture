using CleanArchitecture.Audit.Domain;
using CleanArchitecture.Audit.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Audit;

public class CommandAuditAgent : AuditAgent<CommandAuditTrail>
{
    private readonly IServiceScopeFactory serviceScopeFactory;

    public CommandAuditAgent(IServiceScopeFactory serviceScopeFactory)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task Log(IEnumerable<CommandAuditTrail> logEntries)
    {
        using var scope = serviceScopeFactory.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<AuditDbContext>();
        db.Set<CommandAuditTrail>().AddRange(logEntries);
        await db.SaveChangesAsync();
    }
}
