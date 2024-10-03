namespace CleanArchitecture.Ordering.Domain.Repositories;

public interface IOrderDbContext : IDisposable, IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    IEnumerable<TEntry> TrackingEntries<TEntry>(TrackingEntityState state);
}