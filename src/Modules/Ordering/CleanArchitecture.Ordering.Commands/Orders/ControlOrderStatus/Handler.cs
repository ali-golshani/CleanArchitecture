using Framework.Mediator;
using Framework.Results;
using Framework.Mediator.Middlewares;

namespace CleanArchitecture.Ordering.Commands.Orders.ControlOrderStatus;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    public async Task<Result<Empty>> Handle(RequestContext<Command> context)
    {
        var request = context.Request;
        var cancellationToken = context.CancellationToken;
        Console.WriteLine();
        await Console.Out.WriteLineAsync("Handle Control Order Status Command");
        return Empty.Value;
    }
}
