using CleanArchitecture.Actors;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class RequestContext<TRequest>
{
    public required Actor Actor { get; init; }
    public required TRequest Request { get; init; }
    public required CancellationToken CancellationToken { get; init; }
}
