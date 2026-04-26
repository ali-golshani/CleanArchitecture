using System.Text.RegularExpressions;

namespace CleanArchitecture.UserManagement.Utilities;

internal static partial class Validation
{
    [GeneratedRegex(@"^[A-Za-z][A-Za-z0-9._]+$", RegexOptions.IgnoreCase, "en-US")]
    public static partial Regex UsernameRegex();

    [GeneratedRegex(@"(?:\+|00)?[1-9]\d{7,14}", RegexOptions.IgnoreCase, "en-US")]
    public static partial Regex MobileNumberRegex();
}
