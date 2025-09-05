using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Example;

public class Command : CommandBase, ICommand<Command, Empty>
{
    public override string RequestTitle => "Example Request";

    public int Id { get; init; }
}
