using Framework.Mediator.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Minimal.Mediator;

namespace Framework.Mediator.Extensions;

public static class Extensions
{
    public static Task<Result<TResponse>> SendByMediator<TRequest, TResponse>(
        this IServiceProvider serviceProvider,
        IRequest<TRequest, TResponse> request,
        CancellationToken cancellationToken)
        where TRequest : IRequest<TRequest, TResponse>
    {
        var mediator = serviceProvider.GetRequiredService<IMediator>();
        return mediator.Send(request, cancellationToken);
    }

    public static TRequest AsRequestType<TRequest, TResponse>(this IRequest<TRequest, TResponse> request)
    {
        if (request is not TRequest result)
        {
            throw new UnexpectedRequestTypeException<TRequest>(request);
        }

        return result;
    }

    public static TRequest AsRequestType<TRequest>(this Request request)
    {
        if (request is not TRequest result)
        {
            throw new UnexpectedRequestTypeException<TRequest>(request);
        }

        return result;
    }

    public static TRequest WithCorrelationId<TRequest>(this TRequest request, Guid? correlationId)
        where TRequest : Request
    {
        request.SetCorrelationId(correlationId);
        return request;
    }
}
