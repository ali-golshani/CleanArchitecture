using CleanArchitecture.Ordering.Commands.Orders.RegisterOrder;
using Framework.Results;
using Infrastructure.CommoditySystem;
using Infrastructure.CommoditySystem.Requests;

namespace CleanArchitecture.Ordering.Runtime.AntiCorruption.CommoditySystem;

internal sealed class CommodityCatalogAcl(ICommoditySystem commoditySystem) : ICommodityCatalog
{
    public async Task<Result<Domain.Orders.Commodity?>> Find(
        int commodityId,
        CancellationToken cancellationToken)
    {
        var result = await commoditySystem.Handle(new GetCommodityRequest
        {
            CommodityId = commodityId,
        }, cancellationToken);

        if (result.IsFailure)
        {
            return result.Errors;
        }

        if (result.Value is null)
        {
            return Result<Domain.Orders.Commodity?>.Success(null);
        }

        return new Domain.Orders.Commodity(
            result.Value.CommodityId,
            result.Value.CommodityName);
    }
}
