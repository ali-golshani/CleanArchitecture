using Microsoft.OpenApi.Models;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerGenOptions>
{
    private const string SampleTokenA = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3MzE5MTIyODQsImV4cCI6MTc2MzQ0ODI4NCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiYWxpZ29sc2hhbmkiLCJHaXZlbk5hbWUiOiJBbGkiLCJTdXJuYW1lIjoiR29sc2hhbmkiLCJ1c2VyX2Rpc3BsYXlfbmFtZSI6IkFsaSBHb2xzaGFuaSIsIlJvbGUiOiJwcm9ncmFtbWVyIiwibmFtZSI6ImFsaWdvbHNoYW5pIn0.12FukcrLXCHtQ96eNnti2iogfopvNFKZoz0CMdVTc4o";
    private const string SampleTokenB = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3MzIwOTUwNzcsImV4cCI6MTc2MzYzMTA3NywiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiYWxpZ29sc2hhbmkiLCJHaXZlbk5hbWUiOiJBbGkiLCJTdXJuYW1lIjoiR29sc2hhbmkiLCJ1c2VyX2Rpc3BsYXlfbmFtZSI6IkFsaSBHb2xzaGFuaSIsIlJvbGUiOiJwcm9ncmFtbWVyIiwibmFtZSI6ImFsaWdvbHNoYW5pIn0.uHl3N89zwiL-OcmTL6rxXc4uRbEcrOwQk-jB-rLjkY4";

    public void Configure(SwaggerGenOptions options)
    {
        options.OperationFilter<RequestPropertiesFilter>();
        options.UseInlineDefinitionsForEnums();
        options.IncludeXmlComments(Assembly.GetEntryAssembly(), true);

        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = $"Clean-Architecture API v{description.ApiVersion}",
                Description = "Clean-Architecture ASP.NET Core Web API",
            });
        }

        AddSecurity(options);
    }

    private static void AddSecurity(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(
            "BearerAuth",
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer",
                Description = 
                    "Input bearer token to access this API. Example:" +
                    "\r\n\r\n" +
                    SampleTokenA +
                    "\r\n\r\n" +
                    SampleTokenB,
            });

        var bearer = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "BearerAuth",
            }
        };

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            [bearer] = []
        });
    }
}