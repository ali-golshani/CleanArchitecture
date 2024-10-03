namespace CleanArchitecture.Shared;

public class PagedResult<T>
{
    public static PagedResult<T> Empty()
    {
        return new PagedResult<T>([], 0);
    }

    public int TotalCount { get; }
    public IReadOnlyCollection<T> Items { get; }

    public PagedResult(IReadOnlyCollection<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }
}
