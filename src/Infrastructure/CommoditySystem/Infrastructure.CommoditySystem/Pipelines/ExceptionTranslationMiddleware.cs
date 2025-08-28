using Framework.Results;
using Minimal.Mediator.Middlewares;

namespace Infrastructure.CommoditySystem.Pipelines;

public sealed class ExceptionTranslationMiddleware<TRequest, TResponse> : IMiddleware<TRequest, Result<TResponse>>
{
    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, Result<TResponse>> next)
    {
        try
        {
            return await next.Handle(context);
        }
        catch (CommoditySystemException)
        {
            throw;
        }
        catch (Exception exp)
        {
            throw new CommoditySystemException(exp);
        }
    }
}