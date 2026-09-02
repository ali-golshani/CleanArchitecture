namespace Framework.Mediator.Middlewares;

public interface IPipeline<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(RequestContext<TRequest> context);
}
