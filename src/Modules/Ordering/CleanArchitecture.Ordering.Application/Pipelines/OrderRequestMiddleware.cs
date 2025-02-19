using CleanArchitecture.Mediator;
using Framework.Mediator;
using Framework.Mediator.Middlewares;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Pipelines;

internal sealed class OrderRequestMiddleware<TRequest, TResponse>
    : IMiddleware<TRequest, TResponse>
    where TRequest : Request, IOrderRequest
{
    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
    {
        await Console.Out.WriteLineAsync(context.Request.OrderId.ToString());
        return await next.Handle(context);
    }
}
