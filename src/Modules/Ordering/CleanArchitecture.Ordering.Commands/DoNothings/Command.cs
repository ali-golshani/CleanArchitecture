using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.DoNothings;

public sealed class Command : CommandBase, ICommand<Command, Empty>
{
    public override string RequestTitle => "Do Nothings Request";

    public int Id { get; init; }
}
