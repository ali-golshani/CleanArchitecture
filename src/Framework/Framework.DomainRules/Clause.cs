using Framework.DomainRules.Extensions;

namespace Framework.DomainRules;

public sealed class Clause(bool isTrue, string statement, params ClauseSource[] sources)
{
    public static Clause InvalidClause(string statement, params ClauseSource[] sources)
    {
        return new Clause(false, statement, sources);
    }

    public static Clause Reverse(bool isFalse, string statement, params ClauseSource[] sources)
    {
        return new Clause(!isFalse, statement, sources);
    }

    public bool IsTrue { get; } = isTrue;
    public string Statement { get; } = statement;
    public ClauseSource[] Sources { get; } = sources;

    public bool IsFalse => !IsTrue;

    public override string ToString()
    {
        return $"{Statement.AppendLine()}{Sources.JoinString()}";
    }
}
