using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.ProcessManager;

internal class ProcessService : IProcessManager
{
    private readonly IRequestHandler requestHandler;

    public ProcessService(IRequestHandler requestHandler)
    {
        this.requestHandler = requestHandler;
    }

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> command, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return await requestHandler.Handle(command, cancellationToken);
    }
}