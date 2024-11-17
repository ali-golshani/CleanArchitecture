namespace Framework.Mediator.Requests;

public interface IRequestMediator
{
    Task<Result<TResponse>> Send<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : IRequest<TRequest, TResponse>;
}