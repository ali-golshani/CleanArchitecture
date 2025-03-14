﻿using CleanArchitecture.Configurations;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CleanArchitecture.WebApi.Shared.Authentication;

public sealed class JwtBearerAuthenticationSettings
{
    public string? SecurityKey { get; set; }
    public string? IntrospectionScheme { get; set; }

    private bool IsReferenceToken => !string.IsNullOrEmpty(IntrospectionScheme);

    public void Configure(
        IConfigurationSection configurationSection,
        AuthenticationBuilder builder,
        string authenticationScheme)
    {
        if (IsReferenceToken)
        {
            builder
            .AddJwtBearer(authenticationScheme, options =>
            {
                Configure(configurationSection, options);
                options.ForwardDefaultSelector = _ => IntrospectionScheme!;
            })
            .AddOAuth2Introspection(IntrospectionScheme, options =>
            {
                Configure(configurationSection, options);
            });
        }
        else
        {
            builder
            .AddJwtBearer(authenticationScheme, options =>
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
    }
}
