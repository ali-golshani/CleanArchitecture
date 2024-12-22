using CleanArchitecture.Configurations;
using CleanArchitecture.Secrets.Exceptions;

namespace CleanArchitecture.Secrets;

public static class ConnectionStrings
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
            SecretsConfiguration.Staging => Properties.Resources.StagingCS,
            SecretsConfiguration.Production => Properties.Resources.ProductionCS,
            SecretsConfiguration.DbMigration => Properties.Resources.DbMigrationCS,
            SecretsConfiguration.Development => Properties.Resources.DevelopmentCS,
            _ => throw new InvalidSecretsConfigurationException(configuration),
        };
    }

    private static bool ShouldDecrypt(SecretsConfiguration onfiguration)
    {
        return onfiguration switch
        {
            SecretsConfiguration.Staging => true,
            SecretsConfiguration.Production => true,
            SecretsConfiguration.DbMigration => true,
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