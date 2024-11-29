namespace CleanArchitecture.Mediator;

public static class Extensions
{
    public static TRequest WithCorrelationId<TRequest>(this TRequest request, Guid? correlationId)
        where TRequest : Request
    {
        request.SetCorrelationId(correlationId);
        return request;
    }
}
