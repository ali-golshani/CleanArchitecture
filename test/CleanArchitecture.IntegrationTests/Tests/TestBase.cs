using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests.Tests;

[TestClass]
public abstract class TestBase
{
    private IServiceScope serviceScope = null!;

    protected static CancellationToken CancellationToken => default;
    protected T Service<T>() where T : notnull => serviceScope.ServiceProvider.GetRequiredService<T>();

    [TestInitialize]
    public void TestInitialize()
    {
        serviceScope = ApplicationServices.CreateScope();
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        await ApplicationServices.FlushAuditAsync();
        serviceScope.Dispose();
    }
}
