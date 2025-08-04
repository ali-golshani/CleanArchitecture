using Framework.DomainRules.Extensions;

namespace Framework.DomainRules;

public sealed class Clause(ClauseResult result, string statement, params ClauseSource[] sources)
{
    public static Clause InvalidClause(string statement, params ClauseSource[] sources)
    {
        return new Clause(ClauseResult.Invalid, statement, sources);
    }

    public Clause(bool isValid, string statement, params ClauseSource[] sources)
        : this(isValid ? ClauseResult.Valid : ClauseResult.Invalid, statement, sources)
    { }

    public ClauseResult Result { get; } = result;
    public string Statement { get; } = statement;
    public ClauseSource[] Sources { get; } = sources;

    public bool IsInvalid => Result == ClauseResult.Invalid;

    public override string ToString()
    {
        return $"{Statement.AppendLine()}{Sources.JoinString()}";
    }
}
