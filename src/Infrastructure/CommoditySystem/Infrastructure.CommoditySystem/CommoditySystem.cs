namespace Infrastructure.CommoditySystem;

public class CommoditySystem : ICommoditySystem
{
    public async Task<Commodity?> GetCommodity(int commodityId)
    {
        await Task.CompletedTask;

        if (commodityId <= 0)
        {
            return null;
        }

        return new Commodity(commodityId, commodityId.ToString());
    }

    public Task<bool> ValidateCustomerCommodity(int customerId, int commodityId)
    {
        var result = commodityId > 0 && customerId > 0 && commodityId + customerId < 100;
        return Task.FromResult(result);
    }
}
