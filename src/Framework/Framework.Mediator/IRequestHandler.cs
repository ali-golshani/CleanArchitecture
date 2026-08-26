namespace Framework.Mediator;

using Framework.Mediator.Middlewares;

public interface IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(RequestContext<TRequest> context);
}
