using Framework.Mediator;

namespace Infrastructure.CommoditySystem.Requests.VerifyCustomerCommodityLicense;

internal sealed class UseCase(IRequestHandler<Request, bool> handler) : UseCase<Request, bool>(handler), IUseCase;