namespace Framework.Messaging;

public interface IOutboxTransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync();
}
