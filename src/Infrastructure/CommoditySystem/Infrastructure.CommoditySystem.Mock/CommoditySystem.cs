using Framework.Mediator;
using Framework.Mediator.Extensions;
using Framework.Results;
using Infrastructure.CommoditySystem.Requests;

namespace Infrastructure.CommoditySystem.Mock;

internal class CommoditySystem(IServiceProvider serviceProvider) : ICommoditySystem
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return serviceProvider.SendToHandler(request, cancellationToken);
    }
}