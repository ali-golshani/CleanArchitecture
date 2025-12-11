using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal sealed class BearerSecuritySchemeTransformer : IOpenApiDocumentTransformer
{
    private const string Lines = "\r\n\r\n";
    private const string SampleTokenA = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3NjA3MzI4NDAsImV4cCI6MTkxODQ5OTI0MCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiYWxpZ29sc2hhbmkiLCJSb2xlIjoicHJvZ3JhbW1lciIsIm5hbWUiOiJhbGlnb2xzaGFuaSIsImRpc3BsYXlOYW1lIjoiQWxpIEdvbHNoYW5pIiwicGVybWlzc2lvbiI6WyJSZWFkT3JkZXJzIiwiUmVnaXN0ZXJPcmRlciJdfQ.D5AF-lUqVOF3ZKJJzSXAV83t97YcvZlOzKwucAG3N6Q";
    private const string SampleTokenB = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3NjA3MzI4NDAsImV4cCI6MTkxODQ5OTI0MCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiYWxpZ29sc2hhbmkiLCJSb2xlIjoicHJvZ3JhbW1lciIsIm5hbWUiOiJhbGlnb2xzaGFuaSIsImRpc3BsYXlOYW1lIjoiQWxpIEdvbHNoYW5pIiwicGVybWlzc2lvbiI6WyJSZWFkT3JkZXJzIiwiUmVnaXN0ZXJPcmRlciJdfQ.W6Dta8f8fsgrMKk6mVAgft0Gk9MxJQytj_0TC4deYfY";

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
                $"{Lines}Schema-A:{Lines}{SampleTokenA}{Lines}Schema-B:{Lines}{SampleTokenB}",
        };
    }
}
