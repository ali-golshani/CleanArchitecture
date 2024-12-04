using CleanArchitecture.Configurations;

namespace CleanArchitecture.Secrets;

public static class Authentication
{
    public static MemoryStream ConfigurationStream(SecretsConfiguration configuration)
    {
        switch (configuration)
        {
            case SecretsConfiguration.Production:
                return ProductionStream();

            case SecretsConfiguration.Staging:
                return StagingStream();

            case SecretsConfiguration.DbMigration:
            case SecretsConfiguration.Development:
            default:
                return DevelopmentStream();
        }
    }

    private static MemoryStream DevelopmentStream()
    {
        var data = Properties.Resources.DevelopmentAuth;
        return new MemoryStream(data);
    }

    private static MemoryStream ProductionStream()
    {
        var text = Properties.Resources.ProductionAuth;
        text = EnvironmentVariables.TryDecrypt(text);
        return ToStream(text);
    }

    private static MemoryStream StagingStream()
    {
        var text = Properties.Resources.StagingAuth;
        text = EnvironmentVariables.TryDecrypt(text);
        return ToStream(text);
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