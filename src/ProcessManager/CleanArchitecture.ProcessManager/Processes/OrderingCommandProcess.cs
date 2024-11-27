using CleanArchitecture.Ordering.Commands;
using Framework.ProcessManager;
using Framework.Results;

namespace CleanArchitecture.ProcessManager.Processes;

internal class OrderingCommandProcess<TRequest, TResponse> : IProcess<TResponse>
    where TRequest : CommandBase, ICommand<TRequest, TResponse>
{
    private readonly ICommandService commandService;
    private readonly TRequest request;

    public OrderingCommandProcess(ICommandService commandService, TRequest request)
    {
        this.commandService = commandService;
        this.request = request;
    }

    public Task<Result<TResponse>> Execute(CancellationToken cancellationToken)
    {
        return commandService.Handle(request, cancellationToken);
    }
}
