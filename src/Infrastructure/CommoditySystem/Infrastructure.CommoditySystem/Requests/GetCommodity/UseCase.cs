using Framework.Mediator;
using Infrastructure.CommoditySystem.Models;

namespace Infrastructure.CommoditySystem.Requests.GetCommodity;

internal sealed class UseCase(IRequestHandler<Request, Commodity?> handler) : UseCase<Request, Commodity?>(handler), IUseCase;