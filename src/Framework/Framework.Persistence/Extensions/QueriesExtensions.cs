using Framework.Queries;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Framework.Persistence.Extensions;

public static class QueriesExtensions
{
    public static async Task<PaginatedItems<T>> Materialize<T>(
        this IQueryable<T> items,
        int pageIndex,
        int pageSize)
    {
        int totalCount = await items.CountAsync();

        List<T> resultItem = await items.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedItems<T>(resultItem, totalCount, pageIndex, pageSize);
    }

    public static async Task<PaginatedItems<TTarget>> Materialize<TSource, TTarget>(
        this IQueryable<TSource> items,
        Expression<Func<TSource, TTarget>> selector,
        int pageIndex,
        int pageSize)
    {
        int totalCount = await items.CountAsync();

        var resultItem = await items.Skip(pageIndex * pageSize).Take(pageSize).Select(selector).ToListAsync();
        return new PaginatedItems<TTarget>(resultItem, totalCount, pageIndex, pageSize);
    }
}
