using Framework.Mediator.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Mediator.Extensions;

public static class Extensions
{
    public static IRequestHandler<TRequest, TResponse> RequestHandler<TRequest, TResponse>(this IServiceProvider serviceProvider)
        where TRequest : IRequest<TRequest, TResponse>
    {
        return serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
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
