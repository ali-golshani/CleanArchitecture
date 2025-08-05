using Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence.Extensions;

public static class CommandsExtensions
{
    public static void LinkCommandCorrelationIds(this DbContextBase db, Guid correlationId)
    {
        foreach (var entity in db.TrackingEntries<CommandAwareEntity>(EntityState.Added))
        {
            entity.InsertCommandCorrelationId = correlationId;
        }

        foreach (var entity in db.TrackingEntries<CommandAwareEntity>(EntityState.Modified))
        {
            entity.UpdateCommandCorrelationId = correlationId;
        }
    }
}
