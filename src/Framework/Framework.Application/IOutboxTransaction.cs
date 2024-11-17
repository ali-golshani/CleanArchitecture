namespace Framework.Application;

public interface IOutboxTransaction : IDisposable
{
    void Commit();
    void Rollback();
}