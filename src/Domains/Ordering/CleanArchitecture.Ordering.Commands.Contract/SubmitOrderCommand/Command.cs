using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.SubmitOrderCommand;

public class Command :
    CommandBase,
    Mediator.IOrderRequest,
    ICommand<Command, Empty>
{
    public override string RequestTitle => "Submit Order";

    public int OrderId { get; init; }
}
