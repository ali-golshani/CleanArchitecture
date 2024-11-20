using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests;

[TestClass]
public class AssemblyInitializer
{
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var services = ServiceCollectionBuilder.Build(out _);
        AssemblyServices.RootServiceProvider = services.BuildServiceProvider();
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        // This method is called once for the test assembly, after all tests are run.
    }

    [TestMethod]
    public void TestMethod()
    {
        Assert.AreEqual(2 * 2, 4);
    }
}
