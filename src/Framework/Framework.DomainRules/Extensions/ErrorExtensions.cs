namespace Framework.DomainRules.Extensions;

public static class ErrorExtensions
{
    public static Clause ToClause(this Error error)
    {
        return Clause.InvalidClause
        (
            statement: error.Message,
            facts: error.Facts
        );
    }
}
