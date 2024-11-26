using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal interface IProcess<TResponse>
{
    Task<Result<TResponse>> Execute(CancellationToken cancellationToken);
}
