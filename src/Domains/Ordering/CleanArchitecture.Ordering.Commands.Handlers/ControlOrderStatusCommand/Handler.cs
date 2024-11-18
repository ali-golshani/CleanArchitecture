using Framework.Mediator.Requests;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.ControlOrderStatusCommand;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    public async Task<Result<Empty>> Handle(Command request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Empty.Value;
    }
}
