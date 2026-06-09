using CleanArchitecture.Ordering.Commands.Resources;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Errors;

public sealed class CommodityNotFoundError(int commodityId) : Error(ErrorType.NotFound, ErrorMessageBuilder.CommodityNotFound(commodityId))
{
    public int CommodityId { get; } = commodityId;
}
