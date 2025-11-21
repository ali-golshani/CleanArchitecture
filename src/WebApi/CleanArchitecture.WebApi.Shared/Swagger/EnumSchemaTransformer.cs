using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public sealed class EnumSchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        if (context.JsonTypeInfo.Type.IsEnum)
        {
            schema.Enum ??= [];
            schema.Enum.Clear();
            foreach (var value in Enum.GetValuesAsUnderlyingType(context.JsonTypeInfo.Type))
            {
                var name = Enum.GetName(context.JsonTypeInfo.Type, value);
                var item = $"{name}: {value}";
                schema.Enum.Add(item);
            }
        }

        return Task.CompletedTask;
    }
}
