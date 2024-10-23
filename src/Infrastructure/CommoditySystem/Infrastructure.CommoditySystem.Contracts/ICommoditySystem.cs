namespace Infrastructure.CommoditySystem;

public interface ICommoditySystem
{
    Task<Commodity?> GetCommodity(int commodityId);
    Task<bool> ValidateCustomerCommodity(int customerId, int commodityId);
}
