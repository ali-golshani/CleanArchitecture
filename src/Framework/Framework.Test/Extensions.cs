namespace Framework.Test;

public static class Extensions
{
    private static readonly Random random = new Random();

    public static T PickRandom<T>(this IReadOnlyList<T> values)
    {
        return values[random.Next(values.Count)];
    }
}
