using CleanArchitecture.Configurations;
using CleanArchitecture.WebApi.Shared.Authentication;

namespace CleanArchitecture.WebApi.Authentication;

internal static class AuthenticationConfigs
{
    public static void Configure(IConfiguration configuration, IServiceCollection services)
    {
        var authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = AuthenticationSchemes.Default;
            options.DefaultChallengeScheme = AuthenticationSchemes.Default;
        });

        foreach (var schema in AuthenticationSchemes.Schemas)
        {
            string sectionPath = ConfigurationSections.Authentication.Schema(schema.Name);
            var section = configuration.GetRequiredSection(sectionPath);

            var settings = new JwtBearerAuthenticationSettings();
            section.Bind(settings);
            settings.Configure(section, authenticationBuilder, schema.Name);
        }
    }
}
