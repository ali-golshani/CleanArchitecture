using Microsoft.OpenApi.Models;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CleanArchitecture.WebApi.Shared.Swagger;

internal class ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerGenOptions>
{
    private const string SampleTokenA = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3NjA3MzI4NDAsImV4cCI6MTkxODQ5OTI0MCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiYWxpZ29sc2hhbmkiLCJSb2xlIjoicHJvZ3JhbW1lciIsIm5hbWUiOiJhbGlnb2xzaGFuaSIsImRpc3BsYXlOYW1lIjoiQWxpIEdvbHNoYW5pIiwicGVybWlzc2lvbiI6WyJSZWFkT3JkZXJzIiwiUmVnaXN0ZXJPcmRlciJdfQ.D5AF-lUqVOF3ZKJJzSXAV83t97YcvZlOzKwucAG3N6Q";
    private const string SampleTokenB = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJPbmxpbmUgSldUIEJ1aWxkZXIiLCJpYXQiOjE3NjA3MzI4NDAsImV4cCI6MTkxODQ5OTI0MCwiYXVkIjoid3d3LmV4YW1wbGUuY29tIiwic3ViIjoiYWxpZ29sc2hhbmkiLCJSb2xlIjoicHJvZ3JhbW1lciIsIm5hbWUiOiJhbGlnb2xzaGFuaSIsImRpc3BsYXlOYW1lIjoiQWxpIEdvbHNoYW5pIiwicGVybWlzc2lvbiI6WyJSZWFkT3JkZXJzIiwiUmVnaXN0ZXJPcmRlciJdfQ.W6Dta8f8fsgrMKk6mVAgft0Gk9MxJQytj_0TC4deYfY";

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