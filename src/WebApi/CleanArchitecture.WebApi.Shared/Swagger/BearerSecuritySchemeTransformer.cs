using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    private const string Lines = "\r\n\r\n";
    private const string SampleTokenB = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3NzUxNjE3NDQsImV4cCI6MTgzODMyMDE0NCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiYWxpZ29sc2hhbmkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJwcm9ncmFtbWVyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFsaWdvbHNoYW5pIiwiZGlzcGxheU5hbWUiOiJBbGkgR29sc2hhbmkiLCJwZXJtaXNzaW9uIjpbIlJlYWRPcmRlcnMiLCJSZWdpc3Rlck9yZGVyIl19.n1e8_g0fN0SMCIlukrXeTkzTzWLF7I1GI8qiYoec6SE";

    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        var securitySchemes = new Dictionary<string, IOpenApiSecurityScheme>
        {
            ["Bearer"] = BearerSchema()
        };
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes = securitySchemes;

        foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations ?? []).Select(x => x.Value))
        {
            operation.Security ??= [];
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        }
    }

    private static OpenApiSecurityScheme BearerSchema()
    {
        return new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
            Description =
                $"Input bearer token to access this API. Example:" +
                $"{Lines}Schema-B:{Lines}{SampleTokenB}",
        };
    }
}
