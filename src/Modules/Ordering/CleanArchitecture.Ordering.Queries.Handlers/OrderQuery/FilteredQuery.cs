namespace CleanArchitecture.Ordering.Queries.OrderQuery;

internal sealed class FilteredQuery : Query
{
    public required int? CustomerId { get; init; }
    public required int? BrokerId { get; init; }
}
