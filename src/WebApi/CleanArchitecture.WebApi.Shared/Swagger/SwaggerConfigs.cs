using Framework.WebApi;
using Scalar.AspNetCore;
using System.Text.Json.Serialization.Metadata;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public static class SwaggerConfigs
{
    private const string DefaultDocumentName = "Default";

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

        AddDefaultDocument(services);

        services.AddEndpointsApiExplorer();
    }

    private static void AddDefaultDocument(IServiceCollection services)
    {
        services.AddOpenApi(DefaultDocumentName, options =>
        {
            var defaultSchemaReferenceId = options.CreateSchemaReferenceId;
            options.CreateSchemaReferenceId = typeInfo => SchemaReferenceId(typeInfo, defaultSchemaReferenceId);

            options.ShouldInclude = x => x.GroupName is null;

            options.AddSchemaTransformer<EnumSchemaTransformer>();
            options.AddSchemaTransformer<RequestReadOnlyPropertiesTransformer>();
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
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

            options.AddDocument(DefaultDocumentName);
        });
        app.UseSwaggerUI(options =>
        {
            foreach (var module in modules)
            {
                var url = $"/openapi/{module.Name}.json";
                options.SwaggerEndpoint(url, module.Name);
            }

            {
                var url = $"/openapi/{DefaultDocumentName}.json";
                options.SwaggerEndpoint(url, DefaultDocumentName);
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
}

