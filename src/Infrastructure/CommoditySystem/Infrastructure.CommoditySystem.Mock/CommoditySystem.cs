using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Mock;

internal class CommoditySystem(Minimal.Mediator.IMediator mediator) : ICommoditySystem
{
    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return mediator.Send(request, cancellationToken);
    }
}