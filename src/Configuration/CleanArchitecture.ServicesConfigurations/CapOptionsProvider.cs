using Framework.Cap;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations;

internal static class CapOptionsProvider
{
    public static CapOptions CapOptions(this IConfiguration configuration)
    {
        var section = configuration.GetSection(Configurations.ConfigurationSections.Cap.Options);
        var options = section.Get<CapOptions>();
        return options ?? Framework.Cap.CapOptions.Default;
    }
}