using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            schema.Enum.Clear();

            foreach (var value in Enum.GetValuesAsUnderlyingType(context.Type))
            {
                var name = Enum.GetName(context.Type, value);
                var item = new OpenApiString($"{name}: {value}");
                schema.Enum.Add(item);
            }
        }
    }
}
