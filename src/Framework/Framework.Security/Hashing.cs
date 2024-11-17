using System.Security.Cryptography;
using System.Text;

namespace Framework.Security;

public static class Hashing
{
    public static string Hash(string clearPassword)
    {
        var data = SHA256.HashData(Encoding.Unicode.GetBytes(clearPassword));
        var hashedPassword = Convert.ToBase64String(data);
        return hashedPassword;
    }

    public static string Hash(string clearPassword, out string passwordSalt)
    {
        passwordSalt = RandomSalt();
        return Hash(clearPassword, passwordSalt);
    }

    public static bool IsValid(string clearPassword, string hashedPassword)
    {
        return Hash(clearPassword) == hashedPassword;
    }

    public static bool IsValid(string clearPassword, string passwordSalt, string saltedHashedPassword)
    {
        return Hash(clearPassword, passwordSalt) == saltedHashedPassword;
    }

    private static string Hash(string clearPassword, string passwordSalt)
    {
        var saltedPassword = clearPassword + passwordSalt;
        var data = SHA256.HashData(Encoding.Unicode.GetBytes(saltedPassword));
        return Convert.ToBase64String(data);
    }

    private static string RandomSalt()
    {
        var rng = RandomNumberGenerator.Create();
        var saltBytes = new byte[16];
        rng.GetBytes(saltBytes);
        return Convert.ToBase64String(saltBytes);
    }
}
