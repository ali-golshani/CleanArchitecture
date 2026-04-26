using Framework.DomainRules.Exceptions;

namespace Framework.DomainRules.Extensions;

public static class ClauseExtensions
{
    public static IEnumerable<Error> Errors(this IEnumerable<Clause> clauses)
    {
        return
            clauses
            .Where(x => x.IsInvalid)
            .Select(ToError);
    }

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
            type: ErrorType.Forbidden,
            message: clause.Statement,
            sources: [.. clause.Sources.Select(ToErrorSource)]
        );
    }

    private static ErrorSource ToErrorSource(ClauseSource source)
    {
        return new ErrorSource(source.Name, source.Value);
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
