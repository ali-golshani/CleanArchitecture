using Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence.Utilities;

public static class CommandCorrelationIdUtility
{
    public static void LinkCommandCorrelationIds(DbContextBase db, Guid correlationId)
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