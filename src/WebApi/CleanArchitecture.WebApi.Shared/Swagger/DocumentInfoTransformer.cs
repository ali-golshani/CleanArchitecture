using Framework.WebApi;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal sealed class DocumentInfoTransformer(IModule module) : IOpenApiDocumentTransformer
{
    private readonly IModule module = module;

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info.Version = module.Version;
        document.Info.Title = $"Clean-Architecture {module.Title} API";
        document.Info.Description = $"Clean-Architecture {module.Title} ASP.NET Core Web API";
        return Task.CompletedTask;
    }
}
