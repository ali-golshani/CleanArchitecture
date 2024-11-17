using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.EmptyTestingCommand;

public class Command :
    CommandBase,
    ICommand<Command, Empty>
{
    public override string RequestTitle => "Empty Testing Request";

    public int Id { get; init; }
}
