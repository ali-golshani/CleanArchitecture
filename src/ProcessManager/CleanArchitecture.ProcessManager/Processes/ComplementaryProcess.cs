using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal sealed class ComplementaryProcess<TResponse> : IProcess<TResponse>
{
    private readonly IProcess<TResponse> mainProcess;
    private readonly Func<Result<TResponse>, IProcess<TResponse>> complementaryProcessFactory;

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
