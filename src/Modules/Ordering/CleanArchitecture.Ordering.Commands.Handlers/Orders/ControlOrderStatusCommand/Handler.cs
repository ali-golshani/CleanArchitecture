using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.Orders.ControlOrderStatusCommand;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    public async Task<Result<Empty>> Handle(Command request, CancellationToken cancellationToken)
    {
        Console.WriteLine();
        await Console.Out.WriteLineAsync("Handle Control Order Status Command");
        return Empty.Value;
    }
}
