using CleanArchitecture.Actors;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.Mediator;

public interface IUseCase<TRequest, TResponse> where TRequest : IRequest<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    Task<Result<TResponse>> Handle(Actor actor, TRequest request, CancellationToken cancellationToken);
}
