using System.Security.Cryptography;
using System.Text;

namespace Framework.Security;

public sealed class DataCrypto(string salt)
{
    public static readonly DataCrypto SecretsCrypto = new("!Secrets-Salt!");

    private const int KeySize = 32; // 256-bit
    private const int NonceSize = 12; // recommended for GCM
    private const int TagSize = 16;
    private const int Iterations = 10_000;

    private static readonly Encoding Encoding = Encoding.UTF8;

    private readonly byte[] salt = Encoding.UTF8.GetBytes(salt);

    public string Encrypt(string plainText, string password)
    {
        var data = Encoding.GetBytes(plainText);
        var encryptedData = Encrypt(data, password);
        return Convert.ToBase64String(encryptedData);
    }

    public byte[] Encrypt(byte[] plainBytes, string password)
    {
        var key = Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: salt,
            iterations: Iterations,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: KeySize);

        var nonce = RandomNumberGenerator.GetBytes(NonceSize);
        var cipher = new byte[plainBytes.Length];
        var tag = new byte[TagSize];

        using var aes = new AesGcm(key, tagSizeInBytes: TagSize);
        aes.Encrypt(nonce, plainBytes, cipher, tag);

        using var ms = new MemoryStream();
        ms.Write(nonce);
        ms.Write(tag);
        ms.Write(cipher);
        return ms.ToArray();
    }

    public string? TryDecrypt(string cipherText, string password)
    {
        try
        {
            return Decrypt(cipherText, password);
        }
        catch
        {
            return null;
        }
    }

    public byte[]? TryDecrypt(byte[] cipherBytes, string password)
    {
        try
        {
            return Decrypt(cipherBytes, password);
        }
        catch
        {
            return null;
        }
    }

    public string Decrypt(string cipherText, string password)
    {
        var encryptedData = Convert.FromBase64String(cipherText);
        var pureData = Decrypt(encryptedData, password);
        return Encoding.GetString(pureData);
    }

    public byte[] Decrypt(byte[] cipherBytes, string password)
    {
        var key = Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: salt,
            iterations: Iterations,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: KeySize);

        var nonce = new byte[NonceSize];
        var tag = new byte[TagSize];
        var cipher = new byte[cipherBytes.Length - NonceSize - TagSize];

        Buffer.BlockCopy(cipherBytes, 0, nonce, 0, NonceSize);
        Buffer.BlockCopy(cipherBytes, NonceSize, tag, 0, TagSize);
        Buffer.BlockCopy(cipherBytes, NonceSize + TagSize, cipher, 0, cipher.Length);

        var plain = new byte[cipher.Length];

        using var aes = new AesGcm(key, tagSizeInBytes: TagSize);
        aes.Decrypt(nonce, cipher, tag, plain);

        return plain;
    }
}