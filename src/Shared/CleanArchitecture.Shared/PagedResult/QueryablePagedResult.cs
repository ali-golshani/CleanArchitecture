namespace CleanArchitecture.Shared;

public class QueryablePagedResult<T>
{
    public int TotalCount { get; }
    public IQueryable<T> Items { get; }

    public QueryablePagedResult(IQueryable<T> items)
    {
        Items = items;
        TotalCount = items.Count();
    }

    public QueryablePagedResult(IQueryable<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }

    public PagedResult<T> Materialize(int? maxCount = null)
    {
        if (maxCount is null)
        {
            return new PagedResult<T>(Items.ToList(), TotalCount);
        }
        else
        {
            return new PagedResult<T>(Items.Take(maxCount.Value).ToList(), TotalCount);
        }
    }
}
