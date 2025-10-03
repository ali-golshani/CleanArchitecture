using Microsoft.OpenApi.Models;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerGenOptions>
{
    private const string SampleTokenA = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3NTk0ODIxMDMsImV4cCI6MTkxNzI0ODUwMywiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiYWxpZ29sc2hhbmkiLCJSb2xlIjoicHJvZ3JhbW1lciIsInBlcm1pc3Npb24iOlsiUmVhZE9yZGVycyIsIlJlZ2lzdGVyT3JkZXIiXSwicGVybWlzc2lvbi1zY29wZSI6Ik9yZGVycyIsIm5hbWUiOiJhbGlnb2xzaGFuaSIsImRpc3BsYXlOYW1lIjoiQWxpIEdvbHNoYW5pIn0.YAvd96Xm2xcRiVSvp4fh3tD0OPwMAyfU-2v1K9zhxwU";
    private const string SampleTokenB = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3NTk0ODIxMDMsImV4cCI6MTkxNzI0ODUwMywiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiYWxpZ29sc2hhbmkiLCJSb2xlIjoicHJvZ3JhbW1lciIsInBlcm1pc3Npb24iOlsiUmVhZE9yZGVycyIsIlJlZ2lzdGVyT3JkZXIiXSwicGVybWlzc2lvbi1zY29wZSI6Ik9yZGVycyIsIm5hbWUiOiJhbGlnb2xzaGFuaSIsImRpc3BsYXlOYW1lIjoiQWxpIEdvbHNoYW5pIn0.EO15PKVjTdOP0QOmcMLTUN0UIUcSEOMIyCW9FzCvCgw";

    public void Configure(SwaggerGenOptions options)
    {
        options.SchemaFilter<EnumSchemaFilter>();
        options.OperationFilter<RequestPropertiesFilter>();
        options.OperationFilter<Vernou.Swashbuckle.HttpResultsAdapter.HttpResultsOperationFilter>();

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
                    "Schema-A:" +
                    "\r\n\r\n" +
                    SampleTokenA +
                    "\r\n\r\n" +
                    "Schema-B:" +
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