namespace Framework.Mediator;

public sealed class RequestContext<TRequest>
{
    public required TRequest Request { get; init; }
    public required CancellationToken CancellationToken { get; init; }
    public required DateTime ExecutionStartTime { get; init; }
}
