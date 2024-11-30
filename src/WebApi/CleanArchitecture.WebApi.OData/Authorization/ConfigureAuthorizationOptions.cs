using CleanArchitecture.WebApi.OData.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.WebApi.OData.Authorization;

internal class ConfigureAuthorizationOptions : IConfigureOptions<AuthorizationOptions>
{
    public void Configure(AuthorizationOptions options)
    {
        Shared.Configs.AuthorizationConfigs.RegisterAuthorizationPolicies(options);

        foreach (var schema in AuthenticationSchemes.Schemas)
        {
            options.AddPolicy(schema.Policy, builder =>
            {
                builder.RequireAuthenticatedUser();
                builder.AddAuthenticationSchemes(schema.Name);
            });
        }
    }
}
