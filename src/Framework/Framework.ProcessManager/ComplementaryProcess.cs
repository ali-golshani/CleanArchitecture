using Framework.Results;

namespace Framework.ProcessManager;

internal sealed class ComplementaryProcess<TResponse> : IProcess<TResponse>
{
    private readonly IProcess<TResponse> mainProcess;
    private readonly Func<Result<TResponse>, IProcess<TResponse>> complementaryProcessFactory;

    public ComplementaryProcess(
        IProcess<TResponse> mainProcess,
        IProcess<TResponse> complementaryProcess)
        : this(mainProcess, _ => complementaryProcess)
    { }

    public ComplementaryProcess(
        IProcess<TResponse> mainProcess,
        Func<Result<TResponse>, IProcess<TResponse>> complementaryProcessFactory)
    {
        this.mainProcess = mainProcess;
        this.complementaryProcessFactory = complementaryProcessFactory;
    }

    public async Task<Result<TResponse>> Execute(CancellationToken cancellationToken)
    {
        var result = await mainProcess.Execute(cancellationToken);

        if (result.IsSuccess)
        {
            var complementary = complementaryProcessFactory(result);
            return await complementary.Execute(cancellationToken);
        }
        else
        {
            return result;
        }
    }
}

internal sealed class ComplementaryProcess<TFirstResponse, TResponse> : IProcess<TResponse>
{
    private readonly IProcess<TFirstResponse> firstProcess;
    private readonly Func<Result<TFirstResponse>, IProcess<TResponse>> complementaryProcessFactory;

    public ComplementaryProcess(
        IProcess<TFirstResponse> mainProcess,
        IProcess<TResponse> complementaryProcess)
        : this(mainProcess, _ => complementaryProcess)
    { }

    public ComplementaryProcess(
        IProcess<TFirstResponse> mainProcess,
        Func<Result<TFirstResponse>, IProcess<TResponse>> complementaryProcessFactory)
    {
        firstProcess = mainProcess;
        this.complementaryProcessFactory = complementaryProcessFactory;
    }

    public async Task<Result<TResponse>> Execute(CancellationToken cancellationToken)
    {
        var result = await firstProcess.Execute(cancellationToken);

        if (result.IsSuccess)
        {
            var complementary = complementaryProcessFactory(result);
            return await complementary.Execute(cancellationToken);
        }
        else
        {
            return result.Errors;
        }
    }
}
