using Framework.Mediator;
using Framework.Results;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Requests.GetCommodity;

internal sealed class UseCase(IRequestHandler<Request, Commodity?> handler) : IUseCase
{
    public Task<Result<Commodity?>> Execute(Request request, CancellationToken cancellationToken) => handler.Handle(request, cancellationToken);
}
