namespace Framework.DomainRules;

public sealed class Clause(bool isValid, string statement, params Fact[] facts)
{
    public static Clause InvalidClause(string statement, params Fact[] facts)
    {
        return new Clause(false, statement, facts);
    }

    public bool IsValid { get; } = isValid;
    public string Statement { get; } = statement;
    public Fact[] Facts { get; } = facts;

    public bool IsInvalid => !IsValid;
}
