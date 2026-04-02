using Framework.WebApi;
using Scalar.AspNetCore;
using System.Text.Json.Serialization.Metadata;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public static class SwaggerConfigs
{
    public static void Configure(IServiceCollection services, IModule[] modules)
    {
        foreach (var module in modules)
        {
            services.AddOpenApi(module.Name, options =>
            {
                var defaultSchemaReferenceId = options.CreateSchemaReferenceId;
                options.CreateSchemaReferenceId = typeInfo => SchemaReferenceId(typeInfo, defaultSchemaReferenceId);

                options.ShouldInclude = x => x.GroupName == module.Name;

                options.AddSchemaTransformer<EnumSchemaTransformer>();
                options.AddSchemaTransformer<RequestReadOnlyPropertiesTransformer>();
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
                options.AddDocumentTransformer(new DocumentInfoTransformer(module));
            });
        }

        services.AddEndpointsApiExplorer();
    }

    public static void Configure(WebApplication app, IModule[] modules)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            foreach (var module in modules)
            {
                options.AddDocument(module.Name);
            }
        });
        app.UseSwaggerUI(options =>
        {
            foreach (var module in modules)
            {
                var url = $"/openapi/{module.Name}.json";
                options.SwaggerEndpoint(url, module.Name);
            }
        });
    }

    private static string? SchemaReferenceId(JsonTypeInfo typeInfo, Func<JsonTypeInfo, string?> defaultSchemaReferenceId)
    {
        var type = typeInfo.Type;

        if (type.IsAssignableTo(typeof(Framework.Mediator.IResponse)) ||
            type.IsAssignableTo(typeof(Framework.Mediator.Request)))
        {
            var typeNamespace = type.Namespace![type.Namespace!.LastIndexOf('.')..][1..];
            return $"{typeNamespace}.{type.Name}";
        }

        return defaultSchemaReferenceId(typeInfo);
    }

    public static void Configure(IServiceCollection services)
    {
        services.AddOpenApi("CleanArchitecture", options =>
        {
            options.AddSchemaTransformer<EnumSchemaTransformer>();
            options.AddSchemaTransformer<RequestReadOnlyPropertiesTransformer>();
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });

        services.AddEndpointsApiExplorer();
    }

    public static void Configure(WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            options.AddDocument("CleanArchitecture");
        });
        app.UseSwaggerUI(options =>
        {
            var url = $"/openapi/CleanArchitecture.json";
            options.SwaggerEndpoint(url, "CleanArchitecture");
        });
    }
}
