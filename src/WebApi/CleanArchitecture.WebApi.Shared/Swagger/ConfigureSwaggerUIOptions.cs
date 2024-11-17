using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal class ConfigureSwaggerUIOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options)
    {
        foreach (var groupName in provider.ApiVersionDescriptions.Select(x => x.GroupName))
        {
            var url = $"/swagger/{groupName}/swagger.json";
            var name = groupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    }
}
