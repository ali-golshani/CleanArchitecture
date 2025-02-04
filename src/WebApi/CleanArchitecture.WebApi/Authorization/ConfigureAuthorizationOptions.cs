using CleanArchitecture.WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.WebApi.Authorization;

internal class ConfigureAuthorizationOptions : IConfigureOptions<AuthorizationOptions>
{
    public void Configure(AuthorizationOptions options)
    {
        PolicyConfigs.RegisterAuthorizationPolicies(options, typeof(Program).Assembly);

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
