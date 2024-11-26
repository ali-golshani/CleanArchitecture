using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal sealed class CompensatorProcess<TResponse> : IProcess<TResponse>
{
    private readonly IProcess<TResponse> mainProcess;
    private readonly Func<Result<TResponse>, IProcess<TResponse>> compensatorProcessFactory;

    public CompensatorProcess(
        IProcess<TResponse> mainProcess,
        Func<Result<TResponse>, IProcess<TResponse>> compensatorProcessFactory)
    {
        this.mainProcess = mainProcess;
        this.compensatorProcessFactory = compensatorProcessFactory;
    }

    public async Task<Result<TResponse>> Execute(CancellationToken cancellationToken)
    {
        var result = await mainProcess.Execute(cancellationToken);

        if (result.IsSuccess)
        {
            return result;
        }
        else
        {
            var compensatorProcess = compensatorProcessFactory(result);
            return await compensatorProcess.Execute(cancellationToken);
        }
    }
}
