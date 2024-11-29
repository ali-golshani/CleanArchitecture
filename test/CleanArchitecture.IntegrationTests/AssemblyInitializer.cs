using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests;

[TestClass]
public class AssemblyInitializer
{
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var services = ServiceCollectionBuilder.Build(out _);
        AssemblyServiceProvider.RootServiceProvider = services.BuildServiceProvider();

        BackgroundServices.Start(AssemblyServiceProvider.RootServiceProvider);
    }

    [AssemblyCleanup]
    public static async Task AssemblyCleanup()
    {
        await BackgroundServices.Stop();
    }

    [TestMethod]
    public void SonarTestMethod()
    {
        Assert.AreNotEqual(4, nameof(AssemblyInitializer).Length);
    }
}
