using System.Security.Cryptography;

namespace Framework.Exceptions.Utilities;

public static class SmallGuid
{
    private static readonly char[] Symbols = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public static string GetUniqueKey(int size = 16)
    {
        return RandomNumberGenerator.GetString(Symbols, size);
    }
}
