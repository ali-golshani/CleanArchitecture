using CleanArchitecture.Secrets.Exceptions;
using Framework.Security;

namespace CleanArchitecture.Secrets;

internal static class EnvironmentVariables
{
    public const string EnvironmentVariablesPrefix = "CleanArchitecture";
    private const string EnvironmentVariablesEncryptionKey = "CleanArchitecture.EKey";

    private static readonly DataCrypto SecretsCrypto = DataCrypto.SecretsCrypto;

    public static string? TryGet(string variable)
    {
        return Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Machine);
    }

    /// <summary>
    /// Throw exception if the environment variable is not found
    /// </summary>
    /// <param name="variable"></param>
    /// <returns></returns>
    /// <exception cref="EnvironmentVariableNotFoundException"></exception>
    public static string Get(string variable)
    {
        return
            Environment.GetEnvironmentVariable(variable, EnvironmentVariableTarget.Machine) ??
            throw new EnvironmentVariableNotFoundException(variable);
    }

    public static string TryDecrypt(string value)
    {
        var key = TryGet(EnvironmentVariablesEncryptionKey);
        return
            key is null ? value :
            SecretsCrypto.TryDecrypt(value, key) ??
            value;
    }

    /// <summary>
    /// Throw exception if the environment variable is not found
    /// Throw exception if the environment variable is not encrypted
    /// </summary>
    /// <param name="variable"></param>
    /// <returns></returns>
    /// <exception cref="EnvironmentVariableNotFoundException"></exception>
    /// <exception cref="InvalidEnvironmentVariableEncryptionException"></exception>
    public static string GetAndDecrypt(string variable)
    {
        var key = Get(EnvironmentVariablesEncryptionKey);
        var value = Get(variable);
        return
            SecretsCrypto.TryDecrypt(value, key) ??
            throw new InvalidEnvironmentVariableEncryptionException(variable);
    }
}
