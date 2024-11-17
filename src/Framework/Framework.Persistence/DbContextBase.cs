using Framework.Threading.BackgroundServices;
using Microsoft.EntityFrameworkCore;

namespace Framework.Persistence;

public abstract class DbContextBase : DbContext
{
    protected DbContextBase() { }

    protected DbContextBase(DbContextOptions options) : base(options) { }

    public virtual IQueryable<TEntity> QuerySet<TEntity>() where TEntity : class
    {
        return Set<TEntity>().AsNoTracking();
    }

    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (Exception exp)
        {
            throw PersistenceException.Translate(exp);
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exp)
        {
            throw PersistenceException.Translate(exp);
        }
    }

    public IEnumerable<TEntry> TrackingEntries<TEntry>(EntityState state)
    {
        return
            ChangeTracker.Entries()
            .Where(x => x.State == state)
            .Where(x => x.Entity is TEntry)
            .Select(x => x.Entity)
            .Cast<TEntry>()
            ;
    }

    protected internal IDistributedLock DistributedLock(string distributedLockName)
    {
        var connectionString = Database.GetConnectionString()!;
        return new SqlDistributedLockWrapper(connectionString, distributedLockName);
    }

    protected bool AnyTrackingChange<TEntity>() where TEntity : class
    {
        return ChangeTracker.Entries<TEntity>().Any(x => x.State switch
        {
            EntityState.Deleted => true,
            EntityState.Modified => true,
            EntityState.Added => true,
            _ => false
        });
    }
}
