using Framework.DomainRules.Exceptions;

namespace Framework.DomainRules.Extensions;

public static class ErrorExtensions
{
    #region clause to error

    public static IEnumerable<Error> Errors(this IEnumerable<Clause> clauses)
    {
        return
            clauses
            .Where(x => x.IsFalse)
            .Select(ToError);
    }

    public static async IAsyncEnumerable<Error> Errors(this IAsyncEnumerable<Clause> clauses)
    {
        await foreach (var clause in clauses)
        {
            if (clause.IsFalse)
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
            sources: clause.Sources.Select(ToErrorSource).ToArray()
        );
    }

    private static ErrorSource ToErrorSource(ClauseSource source)
    {
        return new ErrorSource(source.Name, source.Value);
    }

    #endregion

    #region error to clause

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
            sources: error.Sources.Select(ToClauseSource).ToArray()
        );
    }

    private static ClauseSource ToClauseSource(ErrorSource source)
    {
        return new ClauseSource(source.Name, source.Value);
    }

    #endregion

    #region throw

    public static void Throw(this IEnumerable<Clause> clauses)
    {
        var brokenRules = clauses.Where(x => !x.IsTrue).ToList();

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
            if (!clause.IsTrue)
            {
                brokenRules.Add(clause);
            }
        }

        if (brokenRules.Count > 0)
        {
            throw new DomainRulesException(brokenRules);
        }
    }

    #endregion
}
