using CleanArchitecture.Configurations;
using CleanArchitecture.WebApi.Authorization.Actors;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.WebApi.Shared.Authentication;

public sealed class JwtBearerAuthenticationSettings
{
    public string? SecurityKey { get; set; }
    public string? IntrospectionSchema { get; set; }

    private bool IsReferenceToken => !string.IsNullOrEmpty(IntrospectionSchema);

    public void Configure(
        IConfigurationSection configurationSection,
        AuthenticationBuilder builder,
        string authenticationSchema)
    {
        if (IsReferenceToken)
        {
            builder
            .AddJwtBearer(authenticationSchema, options =>
            {
                Configure(configurationSection, options);
                options.ForwardDefaultSelector = _ => IntrospectionSchema!;
            })
            .AddOAuth2Introspection(IntrospectionSchema, options =>
            {
                Configure(configurationSection, options);
            });
        }
        else
        {
            builder
            .AddJwtBearer(authenticationSchema, options =>
            {
                Configure(configurationSection, options);
            });
        }
    }

    private void Configure(IConfigurationSection settingsSection, JwtBearerOptions options)
    {
        var sectionPath = ConfigurationSections.Authentication.JwtBearerOptions;
        var section = settingsSection.GetSection(sectionPath);
        section.Bind(options);

        options.TokenValidationParameters.ValidIssuer = options.ClaimsIssuer;
        options.TokenValidationParameters.ValidAudience = options.Audience;
        options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
        options.TokenValidationParameters.NameClaimType = ClaimTypes.Username;

        if (!string.IsNullOrEmpty(SecurityKey))
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            options.TokenValidationParameters.IssuerSigningKey = securityKey;
        }
    }

    private static void Configure(IConfigurationSection settingsSection, OAuth2IntrospectionOptions options)
    {
        var sectionPath = ConfigurationSections.Authentication.OAuth2IntrospectionOptions;
        var section = settingsSection.GetSection(sectionPath);
        section.Bind(options);

        options.RoleClaimType = ClaimTypes.Role;
        options.NameClaimType = ClaimTypes.Username;
    }
}
