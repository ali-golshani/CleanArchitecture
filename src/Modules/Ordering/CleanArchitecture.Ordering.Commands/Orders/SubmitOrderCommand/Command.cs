using CleanArchitecture.Mediator;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Orders.SubmitOrderCommand;

public class Command : CommandBase, IOrderRequest, ICommand<Command, Empty>
{
    public override string RequestTitle => "Submit Order";

    public int OrderId { get; init; }
}
