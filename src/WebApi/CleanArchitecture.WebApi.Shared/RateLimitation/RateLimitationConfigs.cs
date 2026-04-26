using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace CleanArchitecture.WebApi.Shared.RateLimitation;

public static class RateLimitationConfigs
{
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
