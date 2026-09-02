namespace Framework.Application;

public interface IOutboxTransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync();
}
