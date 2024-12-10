using CleanArchitecture.Configurations;
using CleanArchitecture.WebApi.OData.Configs;
using CleanArchitecture.WebApi.Shared.Configs;
using CleanArchitecture.WebApi.Shared.Cors;
using CleanArchitecture.WebApi.Shared.Filters;
using CleanArchitecture.WebApi.Shared.Middlewares;
using CleanArchitecture.WebApi.Shared.Swagger;
using CleanArchitecture.WebApi.Shared.Versioning;
using Hellang.Middleware.ProblemDetails;

namespace CleanArchitecture.WebApi.OData;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var services = builder.Services;

        var isDevelopment = builder.Environment.IsDevelopment();
        var isProduction = builder.Environment.IsProduction();
        var isStaging = builder.Environment.IsStaging();

        if (isProduction)
        {
            SystemEnvironment.SetProductionEnvironment();
        }
        else if (isStaging)
        {
            SystemEnvironment.SetStagingEnvironment();
        }

        ServicesConfigurations.Configuration.ConfigureAppConfiguration
        (
            configuration: configuration,
            environment: SystemEnvironment.Environment
        );

        ServicesConfigurations.Configuration.ConfigureServices(services, configuration, SystemEnvironment.Environment);

        services.AddLogging(builder =>
        {
            ServicesConfigurations.Configuration.ConfigureLogging(builder);
        });

        VersioningConfigs.Configure(services);
        CorsConfigs.Configure(services);
        RateLimiterConfigs.Configure(services, RateLimiterConfigs.Fixed);
        SwaggerConfigs.Configure(services);
        ResponseCompressionConfigs.Configure(services);
        FluentValidationConfigs.Configure(services);
        ProblemDetailsConfigs.Configure(services, isDevelopment);

        Authentication.AuthenticationConfigs.Configure(configuration, services);
        Authorization.AuthorizationConfigs.Configure(services);

        services
            .AddControllers(options =>
            {
                options.Filters.Add(new ValidateModelStateAttribute());
            })
            .AddJsonOptions(JsonConfigs.Configure)
            .AddOData();

        services.AddDistributedMemoryCache();

        var app = builder.Build();

        if (isDevelopment)
        {
            SwaggerConfigs.Configure(app);
        }

        app.UseCors();
        app.UseHttpsRedirection();

        if (isDevelopment)
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseProblemDetails();
        }

        app.UseMiddleware<DomainExceptionHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseResponseCompression();
        app.MapControllers();

        app.Run();
    }
}
