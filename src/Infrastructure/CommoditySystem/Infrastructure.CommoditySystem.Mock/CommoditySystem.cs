using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Mock;

internal class CommoditySystem(IRequestHandler requestHandler) : ICommoditySystem
{
    private readonly IRequestHandler requestHandler = requestHandler;

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return requestHandler.Handle(request, cancellationToken);
    }
}