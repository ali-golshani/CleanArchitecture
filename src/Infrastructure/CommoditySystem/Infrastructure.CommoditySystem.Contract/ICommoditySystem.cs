using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.Requests;

namespace Infrastructure.CommoditySystem;

public interface ICommoditySystem
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>;
}
