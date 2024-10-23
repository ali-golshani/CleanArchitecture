namespace CleanArchitecture.Audit.Domain;

public sealed class RequestParameters
{
    public static readonly RequestParameters Empty = new RequestParameters(null);

    public RequestParameters(int? orderId)
    {
        OrderId = orderId;
    }

    public int? OrderId { get; }

    public RequestParameters Update(RequestParameters? response)
    {
        return new RequestParameters(OrderId ??  response?.OrderId);
    }

    public override string ToString()
    {
        return $"{OrderId}";
    }
}
