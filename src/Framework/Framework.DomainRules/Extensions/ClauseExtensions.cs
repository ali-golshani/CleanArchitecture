using Framework.DomainRules.Exceptions;

namespace Framework.DomainRules.Extensions;

public static class ClauseExtensions
{
    public static async IAsyncEnumerable<Error> Errors(this IAsyncEnumerable<Clause> clauses)
    {
        await foreach (var clause in clauses)
        {
            if (clause.IsInvalid)
            {
                yield return ToError(clause);
            }
        }
    }

    private static Error ToError(Clause clause)
    {
        return new Error
        (
            type: ErrorType.Conflict,
            message: clause.Statement,
            facts: clause.Facts
        );
    }

    public static void Throw(this IEnumerable<Clause> clauses)
    {
        var brokenRules = clauses.Where(x => x.IsInvalid).ToList();

        if (brokenRules.Count > 0)
        {
            throw new DomainRulesException(brokenRules);
        }
    }

    public static async Task Throw(this IAsyncEnumerable<Clause> clauses)
    {
        var brokenRules = new List<Clause>();

        await foreach (var clause in clauses)
        {
            if (clause.IsInvalid)
            {
                brokenRules.Add(clause);
            }
        }

        if (brokenRules.Count > 0)
        {
            throw new DomainRulesException(brokenRules);
        }
    }
}
