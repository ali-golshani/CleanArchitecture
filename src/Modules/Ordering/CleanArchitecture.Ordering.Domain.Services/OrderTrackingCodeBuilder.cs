namespace CleanArchitecture.Ordering.Domain.Services;

internal sealed class OrderTrackingCodeBuilder(IDateTime dateTime) : IOrderTrackingCodeBuilder
{
    private readonly IDateTime dateTime = dateTime;

    public string Build()
    {
        return dateTime.Now.Ticks.ToString();
    }
}
