﻿namespace CleanArchitecture.IntegrationTests.Tests;

[TestClass]
public sealed class OrderTest : TestBase
{
    [TestMethod]
    public async Task RegisterOrder()
    {
        var service = Service<Services.RegisterOrderCommandService>();
        var isSuccess = await service.Run();
        Assert.IsTrue(isSuccess);
    }
}