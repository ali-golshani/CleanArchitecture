using Framework.Mediator.Middlewares;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Pipelines;

public sealed class ExceptionTranslationMiddleware<TRequest, TResponse> :
    IMiddleware<TRequest, TResponse>
{
    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IRequestProcessor<TRequest, TResponse> next)
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