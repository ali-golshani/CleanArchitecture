using Microsoft.AspNetCore.OpenApi;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public sealed class ReadOnlyPropertiesTransformer : IOpenApiSchemaTransformer
{
    private static readonly LowerCamelCaser camelCaser = new();

    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (schema?.Properties == null || context.JsonTypeInfo.Type == null || context.ParameterDescription is null)
        {
            return Task.CompletedTask;
        }

        var type = context.JsonTypeInfo.Type;

        var properties = type.GetProperties(
            System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (property.SetMethod is null)
            {
                schema.Properties.Remove(ToLowerCamelCase(property.Name));
            }
        }

        return Task.CompletedTask;
    }

    private static string ToLowerCamelCase(string value)
    {
        return camelCaser.ToLowerCamelCase(value);
    }
}
