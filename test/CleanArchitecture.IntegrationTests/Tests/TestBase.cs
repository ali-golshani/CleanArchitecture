using CleanArchitecture.Actors;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests.Tests;

[TestClass]
public abstract class TestBase
{
    private static IServiceProvider RootServiceProvider => AssemblyServiceProvider.RootServiceProvider;

    private IServiceScope serviceScope = null!;
    private IServiceProvider serviceProvider = null!;

    protected T Service<T>() where T : notnull => serviceProvider.GetRequiredService<T>();

    [TestInitialize]
    public void TestInit()
    {
        serviceScope = RootServiceProvider.CreateScope();
        serviceProvider = serviceScope.ServiceProvider;
    }

    [TestCleanup]
    public void TestCleanup()
    {
        serviceScope.Dispose();
    }
}
