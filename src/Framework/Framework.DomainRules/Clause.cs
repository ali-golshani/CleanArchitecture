namespace Framework.DomainRules;

public sealed class Clause(bool isValid, string statement, params ClauseSource[] sources)
{
    public static Clause InvalidClause(string statement, params ClauseSource[] sources)
    {
        return new Clause(false, statement, sources);
    }

    public bool IsValid { get; } = isValid;
    public string Statement { get; } = statement;
    public ClauseSource[] Sources { get; } = sources;

    public bool IsInvalid => !IsValid;
}
