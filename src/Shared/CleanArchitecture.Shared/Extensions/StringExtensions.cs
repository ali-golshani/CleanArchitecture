namespace CleanArchitecture.Shared;

public static class StringExtensions
{
    public static string Truncate(this string str, int maxLength)
    {
        if (str.Length <= maxLength)
        {
            return str;
        }
        else
        {
            return str.Substring(0, maxLength);
        }
    }
}
