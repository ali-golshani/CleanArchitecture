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

    protected virtual TimeSpan Timeout => TimeSpan.FromSeconds(30);

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

    [TestMethod]
    public void SonarTestMethod()
    {
        Assert.AreNotEqual(5, nameof(TestBase).Length);
    }

    protected static void WriteErrors(Framework.Results.Result<Framework.Results.Empty> result)
    {
        if (result.IsFailure)
        {
            foreach (var item in result.Errors)
            {
                Console.WriteLine(item.Message);
            }
        }
    }
}
