using Framework.Results;

namespace Framework.Mediator.Requests;

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}