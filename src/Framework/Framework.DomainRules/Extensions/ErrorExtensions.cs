namespace Framework.DomainRules.Extensions;

public static class ErrorExtensions
{
    public static IEnumerable<Clause> Clauses(this IEnumerable<Error> errors)
    {
        return errors.Select(ToClause);
    }

    public static async IAsyncEnumerable<Clause> Errors(this IAsyncEnumerable<Error> errors)
    {
        await foreach (var error in errors)
        {
            yield return error.ToClause();
        }
    }

    public static Clause ToClause(this Error error)
    {
        return Clause.InvalidClause
        (
            statement: error.Message,
            sources: [.. error.Sources.Select(ToClauseSource)]
        );
    }

    private static ClauseSource ToClauseSource(ErrorSource source)
    {
        return new ClauseSource(source.Name, source.Value);
    }
}
