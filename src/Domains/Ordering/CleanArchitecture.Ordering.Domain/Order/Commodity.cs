namespace CleanArchitecture.Ordering.Domain;

public class Commodity(int commodityId, string commodityName)
{
    public int CommodityId { get; } = commodityId;
    public string CommodityName { get; } = commodityName;
}
