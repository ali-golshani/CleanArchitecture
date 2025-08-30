using CleanArchitecture.Actors;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands;

public interface IUseCase<TRequest, TResponse> where TRequest : IRequest<TRequest, TResponse>
{
    Task<Result<TResponse>> Execute(TRequest request, CancellationToken cancellationToken);
    Task<Result<TResponse>> Execute(Actor actor, TRequest request, CancellationToken cancellationToken);
}
