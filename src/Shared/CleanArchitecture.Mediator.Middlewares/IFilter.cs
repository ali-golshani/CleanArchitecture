namespace CleanArchitecture.Mediator.Middlewares;

public interface IFilter<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(RequestContext<TRequest> context, IPipe<TRequest, TResponse> pipe);
}
