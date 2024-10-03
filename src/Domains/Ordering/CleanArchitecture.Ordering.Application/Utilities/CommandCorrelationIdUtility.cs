using CleanArchitecture.Ordering.Domain;

namespace CleanArchitecture.Ordering.Application;

internal static class CommandCorrelationIdUtility
{
    public static void LinkCommandCorrelationId(Domain.Repositories.IOrderDbContext db, CommandBase command)
    {
        foreach (var entity in db.TrackingEntries<CommandAwareEntity>(TrackingEntityState.Added))
        {
            entity.InsertCommandCorrelationId = command.CorrelationId;
        }

        foreach (var entity in db.TrackingEntries<CommandAwareEntity>(TrackingEntityState.Modified))
        {
            entity.UpdateCommandCorrelationId = command.CorrelationId;
        }
    }
}