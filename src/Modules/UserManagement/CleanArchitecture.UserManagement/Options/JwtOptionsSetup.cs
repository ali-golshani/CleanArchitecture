using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.UserManagement.Options;

public sealed class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration configuration = configuration;

    public void Configure(JwtOptions options)
    {
        configuration.GetSection(Settings.ConfigurationSections.JwtOptions).Bind(options);
    }
}