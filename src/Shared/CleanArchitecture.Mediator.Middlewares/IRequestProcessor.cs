namespace CleanArchitecture.Mediator.Middlewares;

public interface IRequestProcessor<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(RequestContext<TRequest> context);
}
