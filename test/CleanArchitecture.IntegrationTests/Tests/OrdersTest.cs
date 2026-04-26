using CleanArchitecture.IntegrationTests.Extensions;
using Infrastructure.CommoditySystem.Mock.MockData;

namespace CleanArchitecture.IntegrationTests.Tests;

[TestClass]
public sealed class OrdersTest : TestBase
{
    [TestMethod]
    public async Task RegisterValidOrder()
    {
        var service = Service<Services.OrderingService>();
        var customerId = Customers.ValidValue();
        var commodityId = Commodities.ValidValue().CommodityId;
        var result = await service.RegisterNextOrder(customerId, commodityId, CancellationToken);
        result.AssertSuccess();
    }

    [TestMethod]
    public async Task RegisterAndSubmitOrder()
    {
        var service = Service<Services.OrderingService>();
        var customerId = Customers.ValidValue();
        var commodityId = Commodities.ValidValue().CommodityId;
        var idResult = await service.RegisterNextOrder(customerId, commodityId, CancellationToken);
        idResult.AssertSuccess();

        var emptyResult = await service.SubmitOrder(idResult.Value, CancellationToken);
        emptyResult.AssertSuccess();
    }

    [TestMethod]
    public async Task RegisterOrderInvalidCommodity()
    {
        var service = Service<Services.OrderingService>();
        var customerId = Customers.ValidValue();
        var commodityId = Commodities.InvalidValue().CommodityId;
        var result = await service.RegisterNextOrder(customerId, commodityId, CancellationToken);
        result.AssertFailure();
    }

    [TestMethod]
    public async Task RegisterOrderInvalidCustomer()
    {
        var service = Service<Services.OrderingService>();
        var customerId = Customers.InvalidValue();
        var commodityId = Commodities.ValidValue().CommodityId;
        var result = await service.RegisterNextOrder(customerId, commodityId, CancellationToken);
        result.AssertFailure();
    }
}
