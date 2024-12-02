namespace CleanArchitecture.IntegrationTests.Tests;

[TestClass]
public sealed class OrderTest : TestBase
{
    [TestMethod]
    public async Task RegisterValidOrder()
    {
        var service = Service<Services.RegisterOrderService>();
        var customerId = Infrastructure.CommoditySystem.Mock.Data.Customers.ValidValue();
        var commodityId = Infrastructure.CommoditySystem.Mock.Data.Commodities.ValidValue().CommodityId;
        var result = await service.Register(customerId, commodityId, CancellationToken);
        WriteErrors(result);
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task RegisterOrderInvalidCommodity()
    {
        var service = Service<Services.RegisterOrderService>();
        var customerId = Infrastructure.CommoditySystem.Mock.Data.Customers.ValidValue();
        var commodityId = Infrastructure.CommoditySystem.Mock.Data.Commodities.InvalidValue().CommodityId;
        var result = await service.Register(customerId, commodityId, CancellationToken);
        WriteErrors(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task RegisterOrderInvalidCustomer()
    {
        var service = Service<Services.RegisterOrderService>();
        var customerId = Infrastructure.CommoditySystem.Mock.Data.Customers.InvalidValue();
        var commodityId = Infrastructure.CommoditySystem.Mock.Data.Commodities.ValidValue().CommodityId;
        var result = await service.Register(customerId, commodityId, CancellationToken);
        WriteErrors(result);
        Assert.IsFalse(result.IsSuccess);
    }
}
