using CleanArchitecture.Configurations;

namespace CleanArchitecture.Options;

public static class Options
{
    public static string ConfigurationFile(OptionsConfiguration configuration)
    {
        return $"Options.{configuration}.json";
    }
}