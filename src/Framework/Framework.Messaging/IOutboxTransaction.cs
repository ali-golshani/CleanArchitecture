namespace Framework.Messaging;

public interface IOutboxTransaction : IAsyncDisposable
{
    System.Data.Common.DbTransaction DbTransaction { get; }
    Task CommitAsync(CancellationToken cancellationToken);
    Task RollbackAsync();
}
