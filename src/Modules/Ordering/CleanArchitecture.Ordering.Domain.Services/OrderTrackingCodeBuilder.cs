namespace CleanArchitecture.Ordering.Domain.Services;

internal sealed class OrderTrackingCodeBuilder(IClock clock) : IOrderTrackingCodeBuilder
{
    private readonly IClock clock = clock;

    public string Build()
    {
        return clock.Now.Ticks.ToString();
    }
}
