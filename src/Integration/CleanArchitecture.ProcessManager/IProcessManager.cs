using Framework.Results;

namespace CleanArchitecture.ProcessManager;

public interface IProcessManager
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>;
}