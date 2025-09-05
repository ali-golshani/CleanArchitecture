namespace CleanArchitecture.Administration.DebugApp.Services;

internal class ExampleCommandService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = CommandService();

        var result = await service.Handle(Actor, new Ordering.Commands.Example.Command
        {
            Id = 101,
        }, default);

        if (result.IsFailure)
        {
            Console.WriteLine(result.Errors);
        }

        WaitingForUserInput("Press Enter to Exit ...");
    }
}
