namespace CleanArchitecture.Ordering.Application.Pipeline;

internal static class Pipelines
{
    public const string Query = $"{nameof(Ordering)}.{nameof(Queries)}";
    public const string Command = $"{nameof(Ordering)}.{nameof(Commands)}";
}
