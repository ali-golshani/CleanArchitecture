using Framework.Mediator;

namespace CleanArchitecture.Mediator.Middlewares;

public sealed class RequestHandlingProcessor<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
    where TRequest : IRequest<TRequest, TResponse>
{
    private readonly IRequestHandler<TRequest, TResponse> handler;

    public RequestHandlingProcessor(IRequestHandler<TRequest, TResponse> handler)
    {
        this.handler = handler;
    }

    public Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        return handler.Handle(context.Request, context.CancellationToken);
    }
}