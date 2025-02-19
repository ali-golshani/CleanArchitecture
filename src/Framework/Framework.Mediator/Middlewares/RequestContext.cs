namespace Framework.Mediator.Middlewares;

public sealed class RequestContext<TRequest>
{
    public required TRequest Request { get; init; }
    public required CancellationToken CancellationToken { get; init; }
}
