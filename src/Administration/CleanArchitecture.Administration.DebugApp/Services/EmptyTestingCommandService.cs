namespace CleanArchitecture.Administration.DebugApp.Services;

public class EmptyTestingCommandService(IServiceProvider serviceProvider) : ServiceBase(serviceProvider)
{
    public virtual async Task Run()
    {
        var service = CommandService();

        var result = await service.Handle(Actor, new Ordering.Commands.EmptyTestingCommand.Command
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
