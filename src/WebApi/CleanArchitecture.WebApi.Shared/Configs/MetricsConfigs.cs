using Prometheus;

namespace CleanArchitecture.WebApi.Shared.Configs;

public static class MetricsConfigs
{
    public static void Configure(WebApplication app)
    {
        app.UseHttpMetrics();
        app.MapMetrics();
    }
}
