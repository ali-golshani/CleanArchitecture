using Framework.Mediator.Requests;

namespace Framework.Mediator.Extensions;

public static class Extensions
{
    public static TRequest AsRequestType<TRequest, TResponse>(this IRequest<TRequest, TResponse> request)
    {
        if (request is not TRequest result)
        {
            throw new ProgrammerException($"Invalid Request Type Casting ({request.GetType()} to {typeof(TRequest)})");
        }

        return result;
    }
}
