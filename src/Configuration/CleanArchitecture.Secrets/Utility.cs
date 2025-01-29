using CleanArchitecture.Secrets.Exceptions;
using System.Text.RegularExpressions;

namespace CleanArchitecture.Secrets;

internal static partial class Utility
{
    public static string FileContent(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new SecretsConfigurationFileNotFoundException(filePath);
        }

        return File.ReadAllText(filePath);
    }

    public static MemoryStream ToStream(string text)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(text);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    public static bool IsBase64Encoding(string content)
    {
        return Base64Regex().IsMatch(content);
    }

    [GeneratedRegex(@"^[A-Za-z0-9+/=]+$")]
    internal static partial Regex Base64Regex();
}
