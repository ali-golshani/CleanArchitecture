namespace Framework.DomainRules.Extensions;

internal static class StringExtensions
{
    private static readonly string NewLine = Environment.NewLine;

    public static string AppendLine(this string str)
    {
        return str + NewLine;
    }

    public static string JoinString<T>(this IEnumerable<T> values)
    {
        return string.Join(NewLine, values);
    }
}
