namespace CleanArchitecture.UserManagement.Options;

public sealed class TokenLifetimeOptions
{
    public int SmsOtpLifetimeSeconds { get; set; } = 60 * 5;
    public int EmailOtpLifetimeSeconds { get; set; } = 60 * 5;
    public int TokenLifetimeSeconds { get; set; } = 60 * 60 * 24;
    public int RefreshTokenLifetimeHours { get; set; } = 24 * 7;
}
