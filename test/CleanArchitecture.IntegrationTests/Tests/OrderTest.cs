using Infrastructure.CommoditySystem.MockData;

namespace CleanArchitecture.IntegrationTests.Tests;

[TestClass]
public sealed class OrderTest : TestBase
{
    [TestMethod]
    public async Task RegisterValidOrder()
    {
        var service = Service<Services.RegisterOrderService>();
        var customerId = Customers.ValidValue();
        var commodityId = Commodities.ValidValue().CommodityId;
        var result = await service.Register(customerId, commodityId, CancellationToken);
        WriteErrors(result);
        Assert.IsTrue(result.IsSuccess);
    }

    [TestMethod]
    public async Task RegisterOrderInvalidCommodity()
    {
        var service = Service<Services.RegisterOrderService>();
        var customerId = Customers.ValidValue();
        var commodityId = Commodities.InvalidValue().CommodityId;
        var result = await service.Register(customerId, commodityId, CancellationToken);
        WriteErrors(result);
        Assert.IsFalse(result.IsSuccess);
    }

    [TestMethod]
    public async Task RegisterOrderInvalidCustomer()
    {
        var service = Service<Services.RegisterOrderService>();
        var customerId = Customers.InvalidValue();
        var commodityId = Commodities.ValidValue().CommodityId;
        var result = await service.Register(customerId, commodityId, CancellationToken);
        WriteErrors(result);
        Assert.IsFalse(result.IsSuccess);
    }
}
