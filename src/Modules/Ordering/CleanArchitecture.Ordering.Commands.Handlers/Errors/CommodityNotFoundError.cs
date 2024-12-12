using Framework.Results.Errors;

namespace CleanArchitecture.Ordering.Commands.Errors;

public class CommodityNotFoundError(int commodityId) : NotFoundError(PersianDictionary.CommodityDictionary.Commodity, commodityId);
