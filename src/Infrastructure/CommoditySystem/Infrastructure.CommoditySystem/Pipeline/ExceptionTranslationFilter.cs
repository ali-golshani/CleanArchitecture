using CleanArchitecture.Mediator.Middlewares;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Pipeline;

public sealed class ExceptionTranslationFilter<TRequest, TResponse> :
    IFilter<TRequest, TResponse>
{
    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IPipe<TRequest, TResponse> pipe)
    {
        try
        {
            return await pipe.Send(context);
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