using Framework.Mediator;
using Framework.Mediator.Extensions;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Mock;

internal class CommoditySystem(IServiceProvider serviceProvider) : ICommoditySystem
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return serviceProvider.SendToHandler(request, cancellationToken);
    }
}