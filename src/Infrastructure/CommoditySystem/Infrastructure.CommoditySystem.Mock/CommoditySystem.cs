using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Mock;

internal class CommoditySystem(IRequestMediator requestMediator) : ICommoditySystem
{
    private readonly IRequestMediator requestMediator = requestMediator;

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : RequestBase, IRequest<TRequest, TResponse>
    {
        return requestMediator.Send(request, cancellationToken);
    }
}