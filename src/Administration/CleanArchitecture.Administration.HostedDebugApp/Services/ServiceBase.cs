using CleanArchitecture.Actors;
using Framework.DependencyInjection;

namespace CleanArchitecture.Administration.HostedDebugApp.Services;

internal abstract class ServiceBase : ITransientService
{
    protected static readonly Actor Admin = new Programmer("golshani", "Ali Golshani");

    protected static void WriteErrors<T>(Framework.Results.Result<T> result, ConsoleColor foregroundColor = ConsoleColor.Red)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = foregroundColor;

        if (result.IsFailure)
        {
            foreach (var item in result.Errors)
            {
                Console.WriteLine(item.Message);
            }
        }

        Console.ForegroundColor = color;
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
