using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal sealed class DocumentVersionTransformer(string version) : IOpenApiDocumentTransformer
{
    private readonly string version = version;

    public Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        document.Info = new OpenApiInfo
        {
            Version = version,
            Title = $"Clean-Architecture API {version}",
            Description = "Clean-Architecture ASP.NET Core Web API"
        };
        return Task.CompletedTask;
    }
}
