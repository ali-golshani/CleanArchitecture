namespace CleanArchitecture.IntegrationTests;

[TestClass]
public sealed class RegisterOrderTest : TestBase
{
    [TestMethod]
    public async Task RegisterOrder()
    {
        var service = CommandService();

        var result = await service.Handle(Actor, new Ordering.Commands.RegisterOrderCommand.Command
        {
            OrderId = 1010055,
            BrokerId = 5,
            CommodityId = 12,
            CustomerId = 13,
            Price = 1000,
            Quantity = 10,
        }, default);

        Assert.IsTrue(result.IsSuccess);
    }
}
