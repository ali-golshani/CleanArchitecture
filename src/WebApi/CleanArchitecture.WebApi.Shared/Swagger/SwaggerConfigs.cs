using CleanArchitecture.WebApi.Shared.Versioning;
using Scalar.AspNetCore;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public static class SwaggerConfigs
{
    public static void Configure(IServiceCollection services)
    {
        foreach (var version in Versions.AllVersions)
        {
            services.AddOpenApi(version, options =>
            {
                options.AddSchemaTransformer<EnumSchemaTransformer>();
                options.AddOperationTransformer<RequestReadOnlyPropertiesTransformer>();
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
                options.AddDocumentTransformer(new DocumentVersionTransformer(version));
            });
        }
        //services.AddEndpointsApiExplorer();
    }

    public static void Configure(WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            foreach (var version in Versions.AllVersions)
            {
                options.AddDocument(version);
            }
        });
        app.UseSwaggerUI(options =>
        {
            foreach (var version in Versions.AllVersions)
            {
                var url = $"/openapi/{version}.json";
                var name = version.ToUpperInvariant();
                options.SwaggerEndpoint(url, name);
            }
        });
    }
}
