using CleanArchitecture.Actors;
using CleanArchitecture.Actors.Extensions;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Administration.HostedDebugApp.Services;

internal abstract class ServiceBase : IService
{
    private readonly IServiceProvider serviceProvider;
    protected static readonly Actor Actor = new Programmer("golshani", "Ali");

    protected ServiceBase(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
        this.serviceProvider.ResolveActor(Actor);
    }

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

    protected static void Exit()
    {
        Console.WriteLine("Please Wait...");
        Thread.Sleep(1000);
        Console.Write("Press Ctrl + C to exit ...");
    }
}
