namespace Framework.Mediator;

public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}

public interface IRequestHandler
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : IRequest<TRequest, TResponse>;
}