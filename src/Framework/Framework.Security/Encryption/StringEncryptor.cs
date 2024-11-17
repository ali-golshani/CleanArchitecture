using System.Text;

namespace Framework.Security.Encryption;

public class StringEncryptor
{
    private readonly Encoding encoding;
    private readonly SymmetricDataEncrypting encrypting;

    public StringEncryptor(string encryptionKey, int seed = 1984)
    {
        encoding = Encoding.UTF8;
        var password = encoding.GetBytes(encryptionKey);
        encrypting = new SymmetricDataEncrypting(password, seed);
    }

    public string Encrypt(string plainText)
    {
        var data = encoding.GetBytes(plainText);
        var encryptedData = encrypting.Encrypt(data);
        return Convert.ToBase64String(encryptedData);
    }

    public string? Decrypt(string cipherText)
    {
        var encryptedData = Convert.FromBase64String(cipherText);

        if (!encrypting.Decrypt(encryptedData, out var pureData))
        {
            return null;
        }

        return encoding.GetString(pureData!);
    }
}
