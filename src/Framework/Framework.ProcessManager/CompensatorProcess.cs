using Framework.Results;

namespace Framework.ProcessManager;

internal sealed class CompensatorProcess<TResponse> : IProcess<TResponse>
{
    private readonly IProcess<TResponse> mainProcess;
    private readonly Func<Result<TResponse>, IProcess<TResponse>> compensatorProcessFactory;

    public CompensatorProcess(
        IProcess<TResponse> mainProcess,
        IProcess<TResponse> compensatorProcess)
        : this(mainProcess, _ => compensatorProcess)
    { }

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

        if (result.IsFailure)
        {
            var compensatorProcess = compensatorProcessFactory(result);
            await compensatorProcess.Execute(cancellationToken);
        }

        return result;
    }
}
