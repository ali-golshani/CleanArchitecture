namespace CleanArchitecture.Configurations;

public static class ConfigurationSections
{
    public static class ConnectionStrings
    {
        public const string CleanArchitectureDb = "ConnectionString";
    }

    public static class Cap
    {
        public const string Options = "Cap";
    }

    public static class DurableTask
    {
        public const string Options = "DurableTask";
        public const string SqlOptions = "DurableTask:Sql";
    }

    public static class Cors
    {
        public const string Origins = "Cors:Origins";
    }

    public static class Authentication
    {
        public const string JwtBearerOptions = "JwtBearerOptions";
        public const string OAuth2IntrospectionOptions = "OAuth2IntrospectionOptions";

        public static string Scheme(string scheme)
        {
            return $"Authentication:{scheme}";
        }
    }
}
