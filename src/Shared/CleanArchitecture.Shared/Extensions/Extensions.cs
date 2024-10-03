using System.Data;
using System.Linq.Expressions;

namespace CleanArchitecture.Shared;

public static class Extensions
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

    public static QueryablePagedResult<TResult> Select<TSource, TResult>(
        this QueryablePagedResult<TSource> source,
        Expression<Func<TSource, TResult>> selector)
    {
        return new QueryablePagedResult<TResult>(source.Items.Select(selector), source.TotalCount);
    }
}
