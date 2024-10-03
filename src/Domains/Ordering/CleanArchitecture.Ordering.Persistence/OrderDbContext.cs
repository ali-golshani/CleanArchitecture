using Framework.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanArchitecture.Ordering.Persistence;

public class OrderDbContext : Framework.Persistence.DatabaseContextBase, Domain.Repositories.IOrderDbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public IEnumerable<TEntry> TrackingEntries<TEntry>(TrackingEntityState state)
    {
        return state switch
        {
            TrackingEntityState.Added => TrackingEntries<TEntry>(EntityState.Added),
            TrackingEntityState.Deleted => TrackingEntries<TEntry>(EntityState.Deleted),
            TrackingEntityState.Modified => TrackingEntries<TEntry>(EntityState.Modified),
            _ => throw new ProgrammerException(),
        };
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
