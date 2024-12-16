namespace CleanArchitecture.Mediator.Middlewares;

public interface IMiddleware<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next);
}
