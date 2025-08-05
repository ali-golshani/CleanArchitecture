using Framework.Domain;
using Framework.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Framework.Persistence.Interceptors;

public sealed class CorrelationIdInterceptor(IRequestContextAccessor requestContextAccessor) : SaveChangesInterceptor
{
    private readonly IRequestContextAccessor requestContextAccessor = requestContextAccessor;

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
        var correlationId = requestContextAccessor.CorrelationId;

        if (correlationId != null && eventData.Context is DbContextBase db)
        {
            db.LinkCommandCorrelationIds(correlationId.Value);
        }
    }
}
