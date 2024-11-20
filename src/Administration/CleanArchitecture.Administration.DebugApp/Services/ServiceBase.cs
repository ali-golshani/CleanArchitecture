using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.DebugApp;

public abstract class ServiceBase(IServiceProvider serviceProvider) : IService
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    protected static readonly Actor Actor = new Programmer("golshani", "Ali");

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

}
