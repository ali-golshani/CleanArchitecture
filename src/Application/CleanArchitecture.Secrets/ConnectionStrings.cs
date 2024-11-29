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
        switch (configuration)
        {
            default:
                throw new InvalidSecretsConfigurationException(configuration);

            case SecretsConfiguration.Development:
                return Properties.Resources.DevelopmentCS;

            case SecretsConfiguration.Staging:
                return Properties.Resources.StagingCS;

            case SecretsConfiguration.Production:
                return Properties.Resources.ProductionCS;

            case SecretsConfiguration.DbMigration:
                return Properties.Resources.DbMigrationCS;
        }
    }

    private static bool ShouldDecrypt(SecretsConfiguration onfiguration)
    {
        switch (onfiguration)
        {
            default:
            case SecretsConfiguration.Development:
            case SecretsConfiguration.Staging:
                return false;

            case SecretsConfiguration.Production:
            case SecretsConfiguration.DbMigration:
                return true;
        }
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