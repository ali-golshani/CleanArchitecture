using Framework.Mediator.Requests;
using Framework.Results;

namespace CleanArchitecture.ProcessManager;

internal class CommandService : ICommandService
{
    private readonly IRequestMediator requestHandler;

    public CommandService(IRequestMediator requestHandler)
    {
        this.requestHandler = requestHandler;
    }

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(ICommand<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : CommandBase, ICommand<TRequest, TResponse>
    {
        return await requestHandler.Send(command, cancellationToken);
    }
}