using CleanArchitecture.UserManagement.Application.Pipelines;
using CleanArchitecture.UserManagement.Contracts;
using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Domain.Services;
using CleanArchitecture.UserManagement.Domain.Services.Jwt;
using CleanArchitecture.UserManagement.Domain.Services.Otp;
using CleanArchitecture.UserManagement.Infrastructure;
using CleanArchitecture.UserManagement.Options;
using CleanArchitecture.UserManagement.Persistence;
using CleanArchitecture.UserManagement.Persistence.Repositories;
using Framework.DependencyInjection.Extensions;
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
        services.ConfigureOptions<TokenLifetimeOptionsSetup>();

        services.AddScoped<JwtService>();
        services.AddScoped<TokenLifetimeService>();

        services.AddTransient<SmsOtpChannel>();
        services.AddTransient<EmailOtpChannel>();

        services.AddScoped<SeedData>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISessionRepository, SessionRepository>();
        services.AddTransient<IOneTimeTokenRepository, OneTimeTokenRepository>();
        services.AddTransient<IOneTimePasswordRepository, OneTimePasswordRepository>();

        services.AddTransient<ISmsSender, FakeSmsSender>();
        services.AddTransient<ISmsTextBuilder, DefaultSmsTextBuilder>();

        services.AddTransient<IEmailSender, FakeEmailSender>();
        services.AddTransient<IEmailContentBuilder, DefaultEmailContentBuilder>();

        services.RegisterValidators();
        services.RegisterRequestHandlers();
        services.RegisterSelfTransientServices();

        services.AddTransient<Application.Services.IRequestService, Application.Services.RequestService>();
        services.AddKeyedPipeline<RequestPipeline.Configuration>(typeof(RequestPipeline.Pipeline<,>));
    }
}