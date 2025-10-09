using Prometheus;

namespace CleanArchitecture.WebApi.Shared.Metrics;

public static class MetricsConfigs
{
    public static void Configure(WebApplication app)
    {
        app.UseHttpMetrics();
        app.MapMetrics();
    }
}
