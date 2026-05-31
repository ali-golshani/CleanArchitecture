namespace Framework.DomainRules.Exceptions;

public sealed class DomainRulesException : DomainException
{
    public DomainRulesException(IReadOnlyCollection<Clause> clauses)
        : base(ErrorMessage(clauses))
    {
        Clauses = clauses;
        Messages = [.. Clauses.Select(x => x.Statement)];
    }

    public IReadOnlyCollection<Clause> Clauses { get; }
    public override IReadOnlyCollection<string> Messages { get; }

    public override IEnumerable<(string Name, object? Value)> LogProperties
    {
        get
        {
            foreach (var clause in Clauses)
            {
                yield return (clause.Statement, string.Join(" ; ", clause.Sources));
            }
        }
    }

    private static string ErrorMessage(IReadOnlyCollection<Clause> clauses)
    {
        return string.Join(Environment.NewLine, clauses.Select(x => x.Statement));
    }
}
