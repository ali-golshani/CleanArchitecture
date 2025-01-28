using CleanArchitecture.Configurations;
using CleanArchitecture.Secrets.Exceptions;

namespace CleanArchitecture.Secrets;

public static class Authentication
{
    public static MemoryStream ConfigurationStream(SecretsConfiguration configuration)
    {
        var text = ConfigurationString(configuration);
        if (ShouldDecrypt(configuration))
        {
            text = EnvironmentVariables.TryDecrypt(text);
        }
        return ToStream(text);
    }

    private static string ConfigurationString(SecretsConfiguration configuration)
    {
        return configuration switch
        {
            SecretsConfiguration.Staging => Properties.Resources.StagingAuth,
            SecretsConfiguration.Production => Properties.Resources.ProductionAuth,
            SecretsConfiguration.DbMigration => Properties.Resources.DevelopmentAuth,
            SecretsConfiguration.Development => Properties.Resources.DevelopmentAuth,
            _ => throw new InvalidSecretsConfigurationException(configuration),
        };
    }

    private static bool ShouldDecrypt(SecretsConfiguration onfiguration)
    {
        return onfiguration switch
        {
            SecretsConfiguration.Staging => true,
            SecretsConfiguration.Production => true,
            SecretsConfiguration.DbMigration => false,
            SecretsConfiguration.Development => false,
            _ => false,
        };
    }

    private static MemoryStream ToStream(string text)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(text);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
}