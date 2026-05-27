using CleanArchitecture.Ordering.Commands.Resources;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Errors;

public class CommodityNotFoundError(int commodityId) : Error(ErrorType.NotFound, ErrorMessageBuilder.CommodityNotFound(commodityId))
{
    public int CommodityId { get; } = commodityId;
}
