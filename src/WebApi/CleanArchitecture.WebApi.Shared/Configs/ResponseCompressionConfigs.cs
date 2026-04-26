using Microsoft.AspNetCore.ResponseCompression;

namespace CleanArchitecture.WebApi.Shared.Configs;

public static class ResponseCompressionConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });
    }
}
