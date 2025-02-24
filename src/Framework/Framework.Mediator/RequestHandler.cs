using Framework.Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator;

internal sealed class RequestHandler : IRequestHandler
{
    private readonly IServiceProvider serviceProvider;

    public RequestHandler(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task<Result<TResponse>> Handle<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : IRequest<TRequest, TResponse>
    {
        var handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        return handler.Handle(request.AsRequestType(), cancellationToken);
    }
}