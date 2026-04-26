using Framework.WebApi;
using Scalar.AspNetCore;
using System.Text.Json.Serialization.Metadata;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public static class SwaggerConfigs
{
    private static readonly ModuleDocument DefaultDocument = new()
    {
        Name = "Default",
        Title = "Default",
        Version = "1.0.0"
    };

    public static void Configure(IServiceCollection services, IModule[] modules)
    {
        ModuleDocument[] documents = [.. modules.Select(x => x.Document).Concat([DefaultDocument]).DistinctBy(x => x.Name)];

        foreach (var document in documents)
        {
            services.AddOpenApi(document.Name, options =>
            {
                var defaultSchemaReferenceId = options.CreateSchemaReferenceId;
                options.CreateSchemaReferenceId = typeInfo => SchemaReferenceId(typeInfo, defaultSchemaReferenceId);

                options.ShouldInclude = x => x.GroupName == document.Name;

                options.AddSchemaTransformer<EnumSchemaTransformer>();
                options.AddSchemaTransformer<RequestReadOnlyPropertiesTransformer>();
                options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
                options.AddDocumentTransformer(new DocumentInfoTransformer(document));
            });
        }

        services.AddEndpointsApiExplorer();
    }

    public static void Configure(WebApplication app, IModule[] modules)
    {
        string[] documents = [.. modules.Select(x => x.Document.Name).Concat([DefaultDocument.Name]).Distinct()];

        app.MapOpenApi();
        app.MapScalarApiReference(options =>
        {
            foreach (var document in documents)
            {
                options.AddDocument(document);
            }
        });
        app.UseSwaggerUI(options =>
        {
            options.RoutePrefix = string.Empty;
            options.DocumentTitle = "Clean-Architecture Swagger UI";

            foreach (var document in documents)
            {
                var url = $"/openapi/{document}.json";
                options.SwaggerEndpoint(url, document);
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

