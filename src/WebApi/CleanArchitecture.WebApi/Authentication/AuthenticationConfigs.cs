using CleanArchitecture.WebApi.Shared.Authentication;

namespace CleanArchitecture.WebApi.Authentication;

internal static class AuthenticationConfigs
{
    public static void Configure(IConfiguration configuration, IServiceCollection services)
    {
        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = AuthenticationSchemes.SchemeA;
            options.DefaultChallengeScheme = AuthenticationSchemes.SchemeA;
        });

        foreach (var scheme in AuthenticationSchemes.Schemes.Select(x => x.Name))
        {
            new JwtBearerAuthenticationSetup(scheme).Configure(authenticationBuilder, configuration);
        }
    }
}
