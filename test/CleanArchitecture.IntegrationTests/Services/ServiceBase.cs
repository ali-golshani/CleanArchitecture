using CleanArchitecture.Actors;
using CleanArchitecture.Ordering.Commands;
using CleanArchitecture.Ordering.Queries;
using Framework.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.IntegrationTests.Services;

internal abstract class ServiceBase(IServiceProvider serviceProvider) : IService
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    protected static readonly Actor Programmer = new Programmer("tester", "Test App");

    protected T Service<T>() where T : notnull => serviceProvider.GetRequiredService<T>();
    protected IQueryService QueryService() => Service<IQueryService>();
    protected ICommandService CommandService() => Service<ICommandService>();

    protected void ResolveActor() => serviceProvider.ResolveActor(Programmer);

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
