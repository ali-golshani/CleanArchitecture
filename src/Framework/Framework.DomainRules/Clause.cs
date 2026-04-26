using Framework.DomainRules.Extensions;

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

    public override string ToString()
    {
        return $"{Statement.AppendLine()}{Sources.JoinString()}";
    }
}
