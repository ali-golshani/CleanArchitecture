using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.WebApi.Shared.Cors;

internal sealed class ConfigureCorsOptions(IConfiguration configuration)
    : IConfigureOptions<CorsOptions>
{
    private readonly IConfiguration configuration = configuration;

    public void Configure(CorsOptions options)
    {
        var origins = configuration.GetSection("Cors:Origins").Get<string[]>() ?? [];

        options.AddDefaultPolicy(builder =>
        {
            builder
                .WithOrigins(origins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    }
}
