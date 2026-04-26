using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.Ordering.Commands.DoNothings;

internal sealed class Handler : IRequestHandler<Command, Empty>
{
    public async Task<Result<Empty>> Handle(Command request, CancellationToken cancellationToken)
    {
        Console.WriteLine();
        await Console.Out.WriteLineAsync("Do Nothings");
        return Empty.Value;
    }
}
