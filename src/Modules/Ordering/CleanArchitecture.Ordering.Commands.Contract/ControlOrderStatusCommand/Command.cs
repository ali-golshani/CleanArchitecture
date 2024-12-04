using CleanArchitecture.Mediator;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.ControlOrderStatusCommand;

public class Command :
    CommandBase,
    IOrderRequest,
    ICommand<Command, Empty>
{
    public override string RequestTitle => "Control Order Status";

    public int OrderId { get; init; }
}
