namespace CleanArchitecture.UserManagement;

public static class Settings
{
    public const int OtpLength = 6;
    public const int MaxOtpAttempts = 2;

    public static class ConfigurationSections
    {
        public const string JwtOptions = "UserManagement:JwtBearer";
        public const string TokenLifetimeOptions = "UserManagement:TokenLifetimes";
    }
}
