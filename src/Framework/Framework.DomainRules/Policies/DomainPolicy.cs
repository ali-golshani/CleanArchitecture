namespace Framework.DomainRules.Policies;

public sealed class DomainPolicy
{
    public IDomainRule[] DomainRules { get; }
    public IAsyncDomainRule[] AsyncDomainRules { get; }

    public DomainPolicy(params IDomainRule[] domainRules)
        : this(domainRules, [])
    { }

    public DomainPolicy(params IAsyncDomainRule[] asyncDomainRules)
        : this([], asyncDomainRules)
    { }

    public DomainPolicy(IDomainRule[] domainRules, params IAsyncDomainRule[] asyncDomainRules)
    {
        this.DomainRules = domainRules;
        this.AsyncDomainRules = asyncDomainRules;
    }

    public async IAsyncEnumerable<Clause> Evaluate()
    {
        foreach (var rule in DomainRules)
        {
            foreach (var clause in rule.Evaluate())
            {
                yield return clause;
            }
        }

        foreach (var rule in AsyncDomainRules)
        {
            await foreach (var clause in rule.Evaluate())
            {
                yield return clause;
            }
        }
    }
}
