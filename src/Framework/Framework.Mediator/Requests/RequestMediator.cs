using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator.Requests;

internal sealed class RequestMediator : IRequestMediator
{
    private readonly IServiceProvider serviceProvider;

    public RequestMediator(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public Task<Result<TResponse>> Send<TRequest, TResponse>(IRequest<TRequest, TResponse> request, CancellationToken cancellationToken)
        where TRequest : IRequest<TRequest, TResponse>
    {
        var handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        if (request is not TRequest req) throw new ProgrammerException();
        return handler.Handle(req, cancellationToken);
    }
}