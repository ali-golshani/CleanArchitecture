using Framework.Mediator;

namespace CleanArchitecture.Ordering.Commands;

public abstract class CommandBase : Command
{
    private protected CommandBase() { }

    public override string LoggingDomain => nameof(Ordering);
}
