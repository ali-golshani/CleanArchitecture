using Framework.Results;

namespace CleanArchitecture.ProcessManager;

public interface IRequestProcessor<in TRequest, TResponse>
    where TRequest : Framework.Mediator.Requests.IRequest<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}