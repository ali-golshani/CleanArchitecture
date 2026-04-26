namespace Framework.Application;

public interface IOutboxTransaction : IAsyncDisposable
{
    Task CommitAsync();
    Task RollbackAsync();
}