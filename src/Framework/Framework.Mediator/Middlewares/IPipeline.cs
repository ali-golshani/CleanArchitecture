namespace Framework.Mediator.Middlewares;

public interface IPipeline<in TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
}
