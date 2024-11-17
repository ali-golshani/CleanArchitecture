using System.Security.Cryptography;

namespace Framework.Security.Encryption;

internal sealed class SymmetricDataEncrypting : IDisposable
{
    private readonly SymmetricAlgorithm algorithm;
    private readonly ICryptoTransform encryptor;
    private readonly ICryptoTransform decryptor;

    public SymmetricDataEncrypting(byte[] password, int seed)
    : this(CreateAlgorithm(password, seed))
    { }

    private SymmetricDataEncrypting(SymmetricAlgorithm algorithm)
    {
        this.algorithm = algorithm;
        encryptor = algorithm.CreateEncryptor();
        decryptor = algorithm.CreateDecryptor();
    }

    public byte[] Encrypt(byte[] data)
    {
        using var memory = new MemoryStream();
        using var cryptoStream = new CryptoStream(memory, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(data, 0, data.Length);
        cryptoStream.FlushFinalBlock();

        return memory.ToArray();
    }

    public bool Decrypt(byte[] encryptedData, out byte[]? pureData)
    {
        try
        {
            pureData = Decrypt(encryptedData);
            return true;
        }
        catch
        {
            pureData = null;
            return false;
        }
    }

    private byte[] Decrypt(byte[] cipherData)
    {
        using var memory = new MemoryStream();
        using var cryptoStream = new CryptoStream(memory, decryptor, CryptoStreamMode.Write);

        cryptoStream.Write(cipherData, 0, cipherData.Length);
        cryptoStream.FlushFinalBlock();

        return memory.ToArray();
    }

    public void Dispose()
    {
        algorithm.Dispose();
    }

    private static Aes CreateAlgorithm(byte[] password, int seed)
    {
        var pdb = CreatePDB(password, seed);

        var algorithm = Aes.Create();
        algorithm.Key = pdb.GetBytes(32);
        algorithm.IV = pdb.GetBytes(16);

        return algorithm;
    }

    private static PasswordDeriveBytes CreatePDB(byte[] password, int seed)
    {
        var salt = new SimpleRandom((uint)seed).RandomSalt(13);
        return new PasswordDeriveBytes(password, salt);
    }
}
