using CleanArchitecture.Configurations;
using CleanArchitecture.WebApi.OData.Configs;
using CleanArchitecture.WebApi.Shared.Configs;
using CleanArchitecture.WebApi.Shared.Cors;
using CleanArchitecture.WebApi.Shared.Filters;
using CleanArchitecture.WebApi.Shared.Middlewares;
using CleanArchitecture.WebApi.Shared.RateLimitation;
using CleanArchitecture.WebApi.Shared.Swagger;
using CleanArchitecture.WebApi.Shared.Versioning;
using Hellang.Middleware.ProblemDetails;

namespace CleanArchitecture.WebApi.OData;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        var services = builder.Services;

        var isDevelopment = builder.Environment.IsDevelopment();

        ServicesConfigurations.Configuration.SetEnvironment(ApplicationFlavor.ODataWebApi, builder.Environment);
        ServicesConfigurations.Configuration.ConfigureAppConfiguration(configuration, SystemEnvironment.Environment);
        ServicesConfigurations.Configuration.AddAppSettings(configuration, builder.Environment);
        ServicesConfigurations.Configuration.ConfigureServices(services, configuration, SystemEnvironment.Environment);

        services.AddLogging(ServicesConfigurations.Configuration.ConfigureLogging);

        VersioningConfigs.Configure(services);
        CorsConfigs.Configure(services);
        RateLimitationConfigs.Configure(services, RateLimiters.Fixed);
        SwaggerConfigs.Configure(services);
        ResponseCompressionConfigs.Configure(services);
        ProblemDetailsConfigs.Configure(services, isDevelopment);

        Authentication.AuthenticationConfigs.Configure(configuration, services);
        Authorization.AuthorizationConfigs.Configure(services);
        Actors.WebApi.ServiceConfigurations.RegisterHttpActorsServices(services);

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

        app.UseHttpsRedirection();
        app.UseResponseCompression();
        app.UseCors();
        app.UseProblemDetails();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        await app.RunAsync();
    }
}
