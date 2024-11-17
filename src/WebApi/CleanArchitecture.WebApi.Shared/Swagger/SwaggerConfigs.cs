namespace CleanArchitecture.WebApi.Shared.Swagger;

public static class SwaggerConfigs
{
    public static void Configure(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerGenOptions>();
        services.ConfigureOptions<ConfigureSwaggerUIOptions>();
    }

    public static void Configure(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
