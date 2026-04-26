namespace Framework.Exceptions.Extensions;

public static class StringExtensions
{
    public static string MultiLineJoin(this IEnumerable<string> values)
    {
        return string.Join(Environment.NewLine, values);
    }
}
