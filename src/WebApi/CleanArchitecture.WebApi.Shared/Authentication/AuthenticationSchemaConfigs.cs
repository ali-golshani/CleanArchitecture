using CleanArchitecture.Configurations;
using Microsoft.AspNetCore.Authentication;

namespace CleanArchitecture.WebApi.Shared.Authentication;

public static class AuthenticationSchemaConfigs
{
    public static void Configure(
        IConfiguration configuration,
        AuthenticationBuilder builder,
        string schema)
    {
        string sectionPath = ConfigurationSections.Authentication.Schema(schema);
        var section = configuration.GetRequiredSection(sectionPath);

        var settings = new JwtBearerAuthenticationSettings();
        section.Bind(settings);
        settings.Configure(section, builder, schema);
    }
}
