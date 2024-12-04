using Framework.Mediator.Requests;
using Framework.Results;

namespace CleanArchitecture.ProcessManager;

internal class ProcessService : IProcessManager
{
    private readonly IRequestMediator requestHandler;

    public ProcessService(IRequestMediator requestHandler)
    {
        this.requestHandler = requestHandler;
    }

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return await requestHandler.Send(command, cancellationToken);
    }
}