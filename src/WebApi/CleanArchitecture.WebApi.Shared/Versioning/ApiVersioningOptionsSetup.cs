using Asp.Versioning;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.WebApi.Shared.Versioning;

internal class ApiVersioningOptionsSetup : IConfigureOptions<ApiVersioningOptions>
{
    public void Configure(ApiVersioningOptions options)
    {
        options.DefaultApiVersion = new ApiVersion(Versions.V1);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }
}
