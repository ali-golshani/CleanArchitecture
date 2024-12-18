namespace CleanArchitecture.Mediator.Middlewares;

public abstract class ConditionalMiddlewareBase<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
{
    protected abstract bool Accept(TRequest request);
    protected abstract Task<Result<TResponse>> InternalHandle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next);

    public Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        return Accept(context.Request) ? InternalHandle(context, next) : next.Handle(context);
    }
}