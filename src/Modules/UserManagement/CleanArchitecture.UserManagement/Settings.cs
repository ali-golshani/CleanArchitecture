namespace CleanArchitecture.UserManagement;

public static class Settings
{
    public const int TokenExpirySeconds = 24 * 60 * 60;

    public static class ConfigurationSections
    {
        public const string JwtOptions = "UserManagement:JwtBearer";
    }
}
