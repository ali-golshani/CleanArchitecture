using Framework.Results;
using Minimal.Mediator;

namespace CleanArchitecture.ProcessManager;

internal class ProcessManager(IMediator mediator) : IProcessManager
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return mediator.Send(request, cancellationToken);
    }
}