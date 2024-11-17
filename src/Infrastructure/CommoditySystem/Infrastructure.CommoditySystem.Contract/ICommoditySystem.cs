using Framework.Mediator.Requests;
using Framework.Results;

namespace Infrastructure.CommoditySystem;

public interface ICommoditySystem
{
    Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>;
}
