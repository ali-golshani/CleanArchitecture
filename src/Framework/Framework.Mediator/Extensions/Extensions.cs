using Framework.Mediator.Exceptions;

namespace Framework.Mediator.Extensions;

public static class Extensions
{
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
