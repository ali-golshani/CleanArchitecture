namespace CleanArchitecture.UserManagement.Utilities;

internal static class RefreshTokenHasher
{
    public static string Hash(string refreshToken)
    {
        return Framework.Security.Hashing.Hash(refreshToken);
    }
}