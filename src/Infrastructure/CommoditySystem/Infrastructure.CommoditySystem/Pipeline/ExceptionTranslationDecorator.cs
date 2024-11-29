using CleanArchitecture.Mediator.Middlewares;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Pipeline;

public sealed class ExceptionTranslationDecorator<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
{
    private readonly IRequestProcessor<TRequest, TResponse> next;

    public ExceptionTranslationDecorator(IRequestProcessor<TRequest, TResponse> next)
    {
        this.next = next;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
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