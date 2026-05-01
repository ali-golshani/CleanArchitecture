using CleanArchitecture.UserManagement.Options;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.UserManagement.Domain.Services;

internal sealed class TokenLifetimeService(IOptions<TokenLifetimeOptions> options)
{
    private readonly TokenLifetimeOptions options = options.Value;

    public int AccessTokenLifetimeSeconds => options.AccessTokenLifetimeSeconds;

    public DateTime SmsOtpExpirationTime(DateTime otpTime)
    {
        return otpTime.AddSeconds(options.SmsOtpLifetimeSeconds);
    }

    public DateTime EmailOtpLifetimeSeconds(DateTime otpTime)
    {
        return otpTime.AddSeconds(options.EmailOtpLifetimeSeconds);
    }

    public DateTime AccessTokenExpirationTime(DateTime tokenTime)
    {
        return tokenTime.AddSeconds(options.AccessTokenLifetimeSeconds);
    }

    public DateTime RefreshTokenExpirationTime(DateTime tokenTime)
    {
        return tokenTime.AddHours(options.RefreshTokenLifetimeHours);
    }
}