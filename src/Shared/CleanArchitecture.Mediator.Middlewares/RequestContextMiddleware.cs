using Framework.Application;
using Framework.Mediator;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class RequestContextMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
    where TRequest : Request
{
    private readonly RequestContextAccessor requestContextAccessor;

    public RequestContextMiddleware(RequestContextAccessor requestContextAccessor)
    {
        this.requestContextAccessor = requestContextAccessor;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        requestContextAccessor.SetContext(context.Request);
        return await next.Handle(context);
    }
}