﻿namespace CleanArchitecture.Configurations;

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

    public static class Cors
    {
        public const string Origins = "Cors:Origins";
    }

    public static class Authentication
    {
        public const string JwtBearerOptions = "JwtBearerOptions";
        public const string OAuth2IntrospectionOptions = "OAuth2IntrospectionOptions";

        public static string Schema(string schema)
        {
            return $"Authentication:{schema}";
        }
    }
}
