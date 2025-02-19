namespace Framework.Mediator.Middlewares;

public interface IRequestContext<out TRequest>
{
    TRequest Request { get; }
    CancellationToken CancellationToken { get; }
}
