using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace CleanArchitecture.WebApi.Shared.Configs;

public static class RateLimiterConfigs
{
    public static readonly FixedWindowRateLimiterOptions Fixed = new FixedWindowRateLimiterOptions
    {
        PermitLimit = 10,
        Window = TimeSpan.FromSeconds(1),
        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
        QueueLimit = 10,
    };

    public static void Configure(IServiceCollection services, FixedWindowRateLimiterOptions options)
    {
        services.AddRateLimiter(rateLimiterOptions =>
        {
            rateLimiterOptions
            .AddFixedWindowLimiter(policyName: "fixed", opt =>
            {
                opt.Window = options.Window;
                opt.AutoReplenishment = options.AutoReplenishment;
                opt.PermitLimit = options.PermitLimit;
                opt.QueueProcessingOrder = options.QueueProcessingOrder;
                opt.QueueLimit = options.QueueLimit;
            });
        });
    }
}
