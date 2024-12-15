namespace CleanArchitecture.Mediator.Middlewares;

public interface IPipe<TRequest, TResponse>
{
    Task<Result<TResponse>> Send(RequestContext<TRequest> context);
}
