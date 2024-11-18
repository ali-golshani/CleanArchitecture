using Framework.Mediator.Requests;
using Framework.Results;

namespace Infrastructure.CommoditySystem;

internal class CommoditySystem(IRequestMediator requestHandler) : ICommoditySystem
{
    private readonly IRequestMediator requestHandler = requestHandler;

    public async Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        try
        {
            return await requestHandler.Send(request, cancellationToken);
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