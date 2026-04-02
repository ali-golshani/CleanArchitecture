using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.UserManagement.Utilities;

internal static class PasswordHasher
{
    private static readonly PasswordHasher<string> passwordHasher = new();

    public static string Hash(string username, string password)
    {
        return passwordHasher.HashPassword(username, password);
    }

    public static bool Verify(string username, string passwordHash, string providedPassword)
    {
        var result = passwordHasher.VerifyHashedPassword(username, passwordHash, providedPassword);
        return result == PasswordVerificationResult.Success;
    }
}