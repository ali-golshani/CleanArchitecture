namespace Framework.Mediator;

public interface IRequestHandler<in TRequest, TResponse> : Minimal.Mediator.IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : IRequest<TRequest, TResponse>;
