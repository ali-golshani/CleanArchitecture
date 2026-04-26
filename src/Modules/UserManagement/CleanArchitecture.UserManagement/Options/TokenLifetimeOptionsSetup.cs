using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.UserManagement.Options;

public sealed class TokenLifetimeOptionsSetup(IConfiguration configuration) : IConfigureOptions<TokenLifetimeOptions>
{
    private readonly IConfiguration configuration = configuration;

    public void Configure(TokenLifetimeOptions options)
    {
        configuration.GetSection(Settings.ConfigurationSections.TokenLifetimeOptions).Bind(options);
    }
}