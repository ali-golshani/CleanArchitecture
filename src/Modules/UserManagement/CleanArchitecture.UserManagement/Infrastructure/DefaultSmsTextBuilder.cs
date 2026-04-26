using CleanArchitecture.UserManagement.Contracts;

namespace CleanArchitecture.UserManagement.Infrastructure;

internal sealed class DefaultSmsTextBuilder : ISmsTextBuilder
{
    public string BuildOtpText(string otp)
    {
        return $"Your OTP is: {otp}";
    }
}
