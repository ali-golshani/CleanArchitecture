using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public abstract class ReadOnlyPropertiesFilter<TModel> : IOperationFilter
{
    protected abstract string[] ReadOnlyProperties { get; }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var parameters =
            context.ApiDescription.ParameterDescriptions
            .Where(IsReadOnly)
            .ToList();

        if (parameters.Count > 0)
        {
            foreach (var parameter in parameters)
            {
                context.ApiDescription.ParameterDescriptions.Remove(parameter);
            }
        }
    }

    private bool IsReadOnly(ApiParameterDescription description)
    {
        var isTModel = description.ModelMetadata?.ContainerType?.IsAssignableTo(typeof(TModel));

        return
            isTModel == true &&
            ReadOnlyProperties.Contains(description.Name);
    }
}
