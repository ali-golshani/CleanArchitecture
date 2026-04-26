using CleanArchitecture.UserManagement.Contracts;

namespace CleanArchitecture.UserManagement.Infrastructure;

internal sealed class DefaultEmailContentBuilder : IEmailContentBuilder
{
    public string BuildOtpContent(string otp)
    {
        return $"Your OTP is: {otp}";
    }
}
