using CleanArchitecture.UserManagement.Application.Pipelines;
using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Domain.Services;
using CleanArchitecture.UserManagement.Options;
using CleanArchitecture.UserManagement.Persistence;
using Framework.Mediator.Extensions;
using Framework.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.UserManagement;

public static class ServicesConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddScoped<JwtService>();

        services.AddTransient<IUserRepository, UserRepository>();

        services.RegisterValidators();
        services.RegisterRequestHandlers();

        services.AddTransient<Application.Services.IRequestService, Application.Services.RequestService>();
        services.AddKeyedPipeline<RequestPipeline.Configuration>(typeof(RequestPipeline.Pipeline<,>));
    }
}