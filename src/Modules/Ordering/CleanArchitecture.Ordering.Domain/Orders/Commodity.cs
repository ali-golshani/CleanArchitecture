namespace CleanArchitecture.Ordering.Domain.Orders;

public sealed class Commodity(int commodityId, string commodityName)
{
    public int CommodityId { get; } = commodityId;
    public string CommodityName { get; } = commodityName;
}
