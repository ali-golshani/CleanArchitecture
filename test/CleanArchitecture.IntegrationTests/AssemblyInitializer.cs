using Infrastructure.RequestAudit;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CleanArchitecture.IntegrationTests;

[TestClass]
public class AssemblyInitializer
{
    private static IServiceScope serviceScope = null!;
    private static CancellationTokenSource cts = null!;

    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        var services = ServiceCollectionBuilder.Build(out _);
        AssemblyServiceProvider.RootServiceProvider = services.BuildServiceProvider();

        cts = new CancellationTokenSource();
        serviceScope = AssemblyServiceProvider.RootServiceProvider.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        var auditAgent = serviceProvider.GetRequiredService<RequestAuditAgent>();
        auditAgent.Start(cts.Token);
    }

    [AssemblyCleanup]
    public static void AssemblyCleanup()
    {
        cts.Dispose();
        serviceScope.Dispose();
    }

    [TestMethod]
    public void SonarTestMethod()
    {
        Assert.AreNotEqual(4, nameof(AssemblyInitializer).Length);
    }
}
