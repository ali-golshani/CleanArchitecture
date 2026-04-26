namespace Framework.Queries;

public class PaginatedItems<T>
{
    public static PaginatedItems<T> Empty(int pageSize = int.MaxValue)
    {
        return new PaginatedItems<T>([], 0, 0, pageSize);
    }

    public IReadOnlyCollection<T> Items { get; }
    public int TotalCount { get; }
    public int PageIndex { get; }
    public int PageSize { get; }

    public PaginatedItems(IReadOnlyCollection<T> items, int totalCount, int pageIndex, int pageSize)
    {
        Items = items;
        TotalCount = totalCount;
        PageIndex = pageIndex;
        PageSize = pageSize;
    }
}
