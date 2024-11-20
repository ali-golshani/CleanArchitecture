namespace CleanArchitecture.IntegrationTests;

[TestClass]
public sealed class RegisterOrderTest : TestBase
{
    [TestMethod]
    public async Task RegisterOrder()
    {
        var service = Service<Services.RegisterOrderCommandService>();
        var isSuccess = await service.Run();
        Assert.IsTrue(isSuccess);
    }
}
