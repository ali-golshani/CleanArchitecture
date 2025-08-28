using CleanArchitecture.Mediator;
using Framework.Mediator;
using Framework.Results;
using Minimal.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal sealed class OrderRequestMiddleware<TRequest, TResponse> : IMiddleware<TRequest, Result<TResponse>>
    where TRequest : Request, IOrderRequest
{
    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, Result<TResponse>> next)
    {
        await Console.Out.WriteLineAsync(context.Request.OrderId.ToString());
        return await next.Handle(context);
    }
}
