using CleanArchitecture.Mediator;
using CleanArchitecture.Mediator.Middlewares;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.Ordering.Application.Pipeline;

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
