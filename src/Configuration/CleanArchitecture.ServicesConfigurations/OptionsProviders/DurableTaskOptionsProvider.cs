using Framework.DurableTask;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.ServicesConfigurations.OptionsProviders;

internal static class DurableTaskOptionsProvider
{
    public static DurableTaskOptions DurableTaskOptions(this IConfiguration configuration)
    {
        var section = configuration.GetSection(Configurations.ConfigurationSections.DurableTask.Options);
        var options = section.Get<DurableTaskOptions>();
        return options ?? new();
    }
}