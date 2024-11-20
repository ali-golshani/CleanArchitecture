using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests;

[TestClass]
public abstract class TestBase
{
    protected static readonly Actor Actor = new Programmer("golshani", "Ali");
    protected static IServiceProvider RootServiceProvider => AssemblyServices.RootServiceProvider;

    protected IServiceScope serviceScope = null!;
    protected IServiceProvider serviceProvider = null!;

    protected T Service<T>() where T : notnull => serviceProvider.GetRequiredService<T>();
    protected IQueryService QueryService() => Service<IQueryService>();
    protected ICommandService CommandService() => Service<ICommandService>();

    protected static void Execute(Action action)
    {
        try
        {
            action();
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp);
        }
    }

    protected static bool WaitingForUserInput([System.Runtime.CompilerServices.CallerMemberName] string? text = null)
    {
        Console.WriteLine();

        if (!string.IsNullOrEmpty(text))
        {
            Console.WriteLine(text);
        }

        Console.Write("Press Any Key to Continue or Esc to Escape ...");
        var input = Console.ReadKey();
        Console.WriteLine();
        return input.Key != ConsoleKey.Escape;
    }

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
