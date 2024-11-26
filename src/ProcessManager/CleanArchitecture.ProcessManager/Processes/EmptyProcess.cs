using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal class EmptyProcess<TResponse> : IProcess<TResponse>
{
    private readonly Result<TResponse> result;

    public EmptyProcess(Result<TResponse> result)
    {
        this.result = result;
    }

    public Task<Result<TResponse>> Execute(CancellationToken cancellationToken)
    {
        return Task.FromResult(result);
    }
}
