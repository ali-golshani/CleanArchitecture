using System.Security.Cryptography;
using System.Text;

namespace Framework.Exceptions.Utilities;

public static class SmallGuid
{
    private static readonly char[] Symbols = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public static string GetUniqueKey(int maxSize = 10)
    {
        var crypto = RandomNumberGenerator.Create();

        byte[] data = new byte[1];
        crypto.GetNonZeroBytes(data);

        data = new byte[maxSize];
        crypto.GetNonZeroBytes(data);

        var result = new StringBuilder(maxSize);

        foreach (var b in data)
        {
            result.Append(Symbols[b % Symbols.Length]);
        }

        return result.ToString();
    }
}
