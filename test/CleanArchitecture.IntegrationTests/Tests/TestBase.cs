using CleanArchitecture.Actors;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests.Tests;

[TestClass]
public abstract class TestBase
{
    private static IServiceProvider RootServiceProvider => AssemblyServiceProvider.RootServiceProvider;

    private IServiceScope serviceScope = null!;
    private IServiceProvider serviceProvider = null!;
    private CancellationTokenSource cts = null!;

    protected CancellationToken CancellationToken => cts.Token;
    protected T Service<T>() where T : notnull => serviceProvider.GetRequiredService<T>();

    protected virtual TimeSpan Timeout => TimeSpan.FromSeconds(1);

    [TestInitialize]
    public void TestInit()
    {
        serviceScope = RootServiceProvider.CreateScope();
        serviceProvider = serviceScope.ServiceProvider;
        cts = new CancellationTokenSource();
        cts.CancelAfter(Timeout);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        cts.Dispose();
        serviceScope.Dispose();
    }
}
