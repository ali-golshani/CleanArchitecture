using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CleanArchitecture.WebApi.Shared.Swagger;

public abstract class ReadOnlyPropertiesTransformer<TModel> : IOpenApiOperationTransformer
{
    private static readonly Lock Sync = new();
    protected abstract string[] ReadOnlyProperties { get; }

    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        lock (Sync)
        {
            Transform(context);
        }

        return Task.CompletedTask;
    }

    private void Transform(OpenApiOperationTransformerContext context)
    {
        var parameters =
            context.Description.ParameterDescriptions
            .Where(IsReadOnly)
            .ToList();

        if (parameters.Count > 0)
        {
            foreach (var parameter in parameters)
            {
                context.Description.ParameterDescriptions.Remove(parameter);
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
