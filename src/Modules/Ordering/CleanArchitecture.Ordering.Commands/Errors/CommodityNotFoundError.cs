using CleanArchitecture.Ordering.Commands.Resources;
using Framework.Results.Errors;

namespace CleanArchitecture.Ordering.Commands.Errors;

public class CommodityNotFoundError(int commodityId) : NotFoundError(ErrorMessageBuilder.CommodityNotFound(commodityId))
{
    public int CommodityId { get; } = commodityId;
}
