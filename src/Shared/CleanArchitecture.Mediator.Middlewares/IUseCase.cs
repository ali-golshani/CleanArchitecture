namespace CleanArchitecture.Mediator.Middlewares;

public interface IUseCase<TRequest, TResponse>
{
    Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context);
}
