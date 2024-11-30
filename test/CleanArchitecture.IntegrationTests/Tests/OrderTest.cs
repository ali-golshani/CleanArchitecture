namespace CleanArchitecture.IntegrationTests.Tests;

[TestClass]
public sealed class OrderTest : TestBase
{
    [TestMethod]
    public async Task RegisterValidOrder()
    {
        var service = Service<Services.RegisterOrderCommandService>();
        Assert.IsTrue(await service.Valid(CancellationToken));
    }

    [TestMethod]
    public async Task RegisterOrderInvalidCommodity()
    {
        var service = Service<Services.RegisterOrderCommandService>();
        Assert.IsFalse(await service.InvalidCommodity(CancellationToken));
    }

    [TestMethod]
    public async Task RegisterOrderInvalidCustomer()
    {
        var service = Service<Services.RegisterOrderCommandService>();
        Assert.IsFalse(await service.InvalidCustomer(CancellationToken));
    }

    protected override TimeSpan Timeout => TimeSpan.FromMinutes(1);
}
