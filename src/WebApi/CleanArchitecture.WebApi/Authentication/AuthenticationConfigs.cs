namespace CleanArchitecture.WebApi.Authentication;

internal static class AuthenticationConfigs
{
    public static void Configure(IConfiguration configuration, IServiceCollection services)
    {
        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = AuthenticationSchemes.SchemaA;
            options.DefaultChallengeScheme = AuthenticationSchemes.SchemaA;
        });

        foreach (var schema in AuthenticationSchemes.Schemas.Select(x => x.Name))
        {
            Shared.Authentication.AuthenticationSchemaConfigs.Configure
            (
                configuration,
                authenticationBuilder,
                schema
            );
        }
    }
}
