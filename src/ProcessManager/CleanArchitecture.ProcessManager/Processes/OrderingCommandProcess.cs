using CleanArchitecture.Ordering.Commands;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal class OrderingCommandProcess<TRequest, TResponse> : IProcess<TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly TRequest request;
    private readonly ICommandService commandService;

    public OrderingCommandProcess(TRequest request, ICommandService commandService)
    {
        this.request = request;
        this.commandService = commandService;
    }

    public Task<Result<TResponse>> Execute(CancellationToken cancellationToken)
    {
        return commandService.Handle(request, cancellationToken);
    }

    public ComplementaryProcess<TResponse> Follow<TComplementaryRequest>(
        Func<Result<TResponse>, TComplementaryRequest> complementaryRequestFactory)
    where TComplementaryRequest : CommandBase, ICommand<TComplementaryRequest, TResponse>
    {
        var complementaryProcessFactory = delegate (Result<TResponse> result)
        {
            var complementaryRequest = complementaryRequestFactory(result);
            return new OrderingCommandProcess<TComplementaryRequest, TResponse>(complementaryRequest, commandService);
        };
        return new ComplementaryProcess<TResponse>(this, complementaryProcessFactory);
    }

    public CompensatorProcess<TResponse> WithCompensator<TCompensatorRequest>(
        Func<Result<TResponse>, TCompensatorRequest> compensatorRequestFactory)
    where TCompensatorRequest : CommandBase, ICommand<TCompensatorRequest, TResponse>
    {
        var compensatorProcessFactory = delegate (Result<TResponse> result)
        {
            var compensatorRequest = compensatorRequestFactory(result);
            return new OrderingCommandProcess<TCompensatorRequest, TResponse>(compensatorRequest, commandService);
        };
        return new CompensatorProcess<TResponse>(this, compensatorProcessFactory);
    }
}
