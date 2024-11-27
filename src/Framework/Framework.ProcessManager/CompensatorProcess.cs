using Framework.Results;

namespace Framework.ProcessManager;

internal sealed class CompensatorProcess<TResponse, TCompensatorResponse> : IProcess<TResponse>
{
    private readonly IProcess<TResponse> mainProcess;
    private readonly Func<Result<TResponse>, IProcess<TCompensatorResponse>> compensatorProcessFactory;

    public CompensatorProcess(
        IProcess<TResponse> mainProcess,
        IProcess<TCompensatorResponse> compensatorProcess)
        : this(mainProcess, _ => compensatorProcess)
    { }

    public CompensatorProcess(
        IProcess<TResponse> mainProcess,
        Func<Result<TResponse>, IProcess<TCompensatorResponse>> compensatorProcessFactory)
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

        var compensatorProcess = compensatorProcessFactory(result);
        var compensatorResult = await compensatorProcess.Execute(cancellationToken);

        if (compensatorResult.IsSuccess)
        {
            return result;
        }
        else
        {
            return result.Errors.Concat(compensatorResult.Errors).ToArray();
        }
    }
}
