using Microsoft.AspNetCore.OpenApi;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public abstract class ReadOnlyPropertiesTransformer<TModel> : IOpenApiSchemaTransformer
{
    private static readonly LowerCamelCaser camelCaser = new();
    protected abstract string[] ReadOnlyProperties { get; }

    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type.IsAssignableTo(typeof(TModel)))
        {
            if (schema.Properties is null)
            {
                return Task.CompletedTask;
            }

            foreach (var property in ReadOnlyProperties)
            {
                schema.Properties.Remove(ToLowerCamelCase(property));
            }
        }

        return Task.CompletedTask;
    }

    protected static string ToLowerCamelCase(string value)
    {
        return camelCaser.ToLowerCamelCase(value);
    }
}
