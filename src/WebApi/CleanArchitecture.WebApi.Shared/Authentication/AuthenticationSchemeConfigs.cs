using CleanArchitecture.Configurations;
using Microsoft.AspNetCore.Authentication;

namespace CleanArchitecture.WebApi.Shared.Authentication;

public static class AuthenticationSchemeConfigs
{
    public static void Configure(
        IConfiguration configuration,
        AuthenticationBuilder builder,
        string scheme)
    {
        string sectionPath = ConfigurationSections.Authentication.Scheme(scheme);
        var section = configuration.GetRequiredSection(sectionPath);

        var settings = new JwtBearerAuthenticationSettings();
        section.Bind(settings);
        settings.Configure(section, builder, scheme);
    }
}
