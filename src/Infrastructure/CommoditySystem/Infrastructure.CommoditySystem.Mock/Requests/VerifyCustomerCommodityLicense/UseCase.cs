using Framework.Mediator;
using Framework.Results;

namespace Infrastructure.CommoditySystem.Requests.VerifyCustomerCommodityLicense;

internal sealed class UseCase(IRequestHandler<Request, bool> handler) : IUseCase
{
    public Task<Result<bool>> Execute(Request request, CancellationToken cancellationToken) => handler.Handle(request, cancellationToken);
}
