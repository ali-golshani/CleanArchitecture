using Framework.Application;
using Framework.Mediator;
using Minimal.Mediator.Middlewares;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class RequestContextMiddleware<TRequest, TResponse> : IMiddleware<TRequest, Result<TResponse>>
    where TRequest : Request
{
    private readonly RequestContextAccessor requestContextAccessor;

    public RequestContextMiddleware(RequestContextAccessor requestContextAccessor)
    {
        this.requestContextAccessor = requestContextAccessor;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, Result<TResponse>> next)
    {
        requestContextAccessor.SetContext(context.Request);
        return await next.Handle(context);
    }
}