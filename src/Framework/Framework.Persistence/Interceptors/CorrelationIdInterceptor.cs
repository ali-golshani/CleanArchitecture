using Framework.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Framework.Persistence.Interceptors;

public sealed class CorrelationIdInterceptor(ICorrelationIdProvider correlationIdProvider) : SaveChangesInterceptor
{
    private readonly ICorrelationIdProvider correlationIdProvider = correlationIdProvider;

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        LinkCommandCorrelationIds(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        LinkCommandCorrelationIds(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void LinkCommandCorrelationIds(DbContextEventData eventData)
    {
        var correlationId = correlationIdProvider.CorrelationId;

        if (correlationId != null && eventData.Context is DbContextBase db)
        {
            db.LinkCommandCorrelationIds(correlationId.Value);
        }
    }
}
