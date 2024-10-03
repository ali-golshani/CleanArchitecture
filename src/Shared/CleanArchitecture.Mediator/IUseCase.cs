namespace CleanArchitecture.Mediator;

public interface IUseCase<in TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    Task<Result<TResponse>> Execute(TRequest request, CancellationToken cancellationToken);
}
