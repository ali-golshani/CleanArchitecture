using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.SampleEmpty;

public class Command : CommandBase, ICommand<Command, Empty>
{
    public override string RequestTitle => "Sample Empty Request";

    public int Id { get; init; }
}
