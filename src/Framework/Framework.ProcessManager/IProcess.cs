using Framework.Results;

namespace Framework.ProcessManager;

public interface IProcess<TResponse>
{
    Task<Result<TResponse>> Execute(CancellationToken cancellationToken);
}
