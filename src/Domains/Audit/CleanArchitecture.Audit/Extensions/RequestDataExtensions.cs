using CleanArchitecture.Audit.Domain;
using CleanArchitecture.Mediator.Requests;

namespace CleanArchitecture.Audit;

internal static class RequestParametersResolver
{
    public static RequestParameters RequestParameters(Request request)
    {
        return new RequestParameters(request?.OrderId());
    }

    public static RequestParameters? ResponseParameters<TResponse>(TResponse _)
    {
        return Domain.RequestParameters.Empty;
    }

    private static int? OrderId(this Request request)
    {
        return (request as IOrderRequest)?.OrderId;
    }
}
