namespace CleanArchitecture.Shared;

public static class AsyncExtensions
{
    public static async IAsyncEnumerable<T> EmptyAsyncEnumerable<T>()
    {
        await ValueTask.CompletedTask.ConfigureAwait(false);
        yield break;
    }

    public static async IAsyncEnumerable<T> Concat<T>(this IAsyncEnumerable<T> first, IAsyncEnumerable<T> second)
    {
        await foreach (var item in first)
        {
            yield return item;
        }

        await foreach (var item in second)
        {
            yield return item;
        }
    }

    public static async IAsyncEnumerable<T> Concat<T>(this IAsyncEnumerable<T> first, IEnumerable<T> second)
    {
        await foreach (var item in first)
        {
            yield return item;
        }

        foreach (var item in second)
        {
            yield return item;
        }
    }

    public static async IAsyncEnumerable<T> Concat<T>(this IEnumerable<T> first, IAsyncEnumerable<T> second)
    {
        foreach (var item in first)
        {
            yield return item;
        }

        await foreach (var item in second)
        {
            yield return item;
        }
    }

    public static async Task<List<TSource>> ToListAsync<TSource>(this IAsyncEnumerable<TSource> source)
    {
        var result = new List<TSource>();
        await foreach (var item in source)
        {
            result.Add(item);
        }
        return result;
    }

    public static async IAsyncEnumerable<TSource> WhereAsync<TSource>(this IEnumerable<TSource> source, Func<TSource, Task<bool>> predicate)
    {
        foreach (var item in source)
        {
            if (await predicate(item))
            {
                yield return item;
            }
        }
    }
}
