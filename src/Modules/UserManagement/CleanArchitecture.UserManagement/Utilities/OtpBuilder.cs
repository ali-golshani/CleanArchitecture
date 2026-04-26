namespace CleanArchitecture.UserManagement.Utilities;

internal static class OtpBuilder
{
    public static string BuildOtp(int length)
    {
        return new string('1', length);
    }
}
