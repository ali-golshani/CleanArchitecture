namespace CleanArchitecture.WebApi.OData.Authentication;

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
            Shared.Authentication.AuthenticationSchemeConfigs.Configure
            (
                configuration,
                authenticationBuilder,
                scheme
            );
        }
    }
}
