namespace Framework.DomainRules;

public sealed class DomainPolicy(params IDomainRule[] domainRules)
{
    public IDomainRule[] DomainRules { get; } = domainRules;

    public IEnumerable<Clause> Evaluate()
    {
        foreach (var rule in DomainRules)
        {
            foreach (var clause in rule.Evaluate())
            {
                yield return clause;
            }
        }
    }
}
