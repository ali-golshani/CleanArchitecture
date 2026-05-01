using CleanArchitecture.Configurations;
using Duende.AspNetCore.Authentication.OAuth2Introspection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.WebApi.Shared.Authentication;

public sealed class JwtBearerAuthenticationSetup(string authenticationScheme)
{
    private readonly string authenticationScheme = authenticationScheme;

    public string? SecurityKey { get; set; }
    public string? IntrospectionScheme { get; set; }

    private bool IsReferenceToken => !string.IsNullOrEmpty(IntrospectionScheme);

    public void Configure(AuthenticationBuilder builder, IConfiguration configuration)
    {
        string sectionPath = ConfigurationSections.Authentication.Scheme(authenticationScheme);
        var schemeSection = configuration.GetRequiredSection(sectionPath);
        schemeSection.Bind(this);

        if (IsReferenceToken)
        {
            builder
            .AddJwtBearer(authenticationScheme, options =>
            {
                Configure(schemeSection, options);
                options.ForwardDefaultSelector = _ => IntrospectionScheme!;
            })
            .AddOAuth2Introspection(IntrospectionScheme!, options =>
            {
                Configure(schemeSection, options);
            });
        }
        else
        {
            builder
            .AddJwtBearer(authenticationScheme, options =>
            {
                Configure(schemeSection, options);
            });
        }
    }

    private void Configure(IConfigurationSection schemeSection, JwtBearerOptions options)
    {
        var sectionPath = ConfigurationSections.Authentication.JwtBearerOptions;
        var section = schemeSection.GetSection(sectionPath);
        section.Bind(options);

        if (!string.IsNullOrEmpty(SecurityKey))
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            options.TokenValidationParameters.IssuerSigningKey = securityKey;
        }
    }

    private static void Configure(IConfigurationSection schemeSection, OAuth2IntrospectionOptions options)
    {
        var sectionPath = ConfigurationSections.Authentication.OAuth2IntrospectionOptions;
        var section = schemeSection.GetSection(sectionPath);
        section.Bind(options);
    }
}
