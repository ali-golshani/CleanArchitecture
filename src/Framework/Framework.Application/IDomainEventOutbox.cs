using Microsoft.EntityFrameworkCore;

namespace Framework.Application;

public interface IDomainEventOutbox
{
    Task<IOutboxTransaction> BeginTransaction(DbContext db, CancellationToken cancellationToken);
    Task Publish<TEvent>(IReadOnlyCollection<TEvent> events, string topic, CancellationToken cancellationToken);
}