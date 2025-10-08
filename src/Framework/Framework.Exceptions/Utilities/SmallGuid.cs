using System.Security.Cryptography;
using System.Text;

namespace Framework.Exceptions.Utilities;

public static class SmallGuid
{
    private static readonly char[] Symbols = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    public static string GetUniqueKey(int size = 10)
    {
        var crypto = RandomNumberGenerator.Create();

        byte[] data = new byte[1];
        crypto.GetNonZeroBytes(data);

        data = new byte[size];
        crypto.GetNonZeroBytes(data);

        var result = new StringBuilder(size);

        foreach (var b in data)
        {
            result.Append(Symbols[b % Symbols.Length]);
        }

        return result.ToString();
    }
}
