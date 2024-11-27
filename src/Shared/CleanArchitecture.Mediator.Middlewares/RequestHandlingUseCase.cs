using Framework.Mediator.Requests;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class RequestHandlingUseCase<TRequest, TResponse> :
    IUseCase<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    private readonly IRequestHandler<TRequest, TResponse> handler;

    public RequestHandlingUseCase(IRequestHandler<TRequest, TResponse> handler)
    {
        this.handler = handler;
    }

    public Task<Result<TResponse>> Handle(UseCaseContext<TRequest> context)
    {
        return handler.Handle(context.Request, context.CancellationToken);
    }
}