using System.Threading.RateLimiting;

namespace CleanArchitecture.WebApi.Shared.RateLimitation;

public static class RateLimiters
{
    public static readonly FixedWindowRateLimiterOptions Fixed = new()
    {
        PermitLimit = 10,
        Window = TimeSpan.FromSeconds(1),
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
        QueueLimit = 10,
    };
}
