namespace Framework.DomainRules.Policies;

public sealed class Policy
{
    public IDomainRule[] DomainRules { get; }
    public IAsyncDomainRule[] AsyncDomainRules { get; }

    public Policy(params IDomainRule[] domainRules)
        : this(domainRules, [])
    { }

    public Policy(params IAsyncDomainRule[] asyncDomainRules)
        : this([], asyncDomainRules)
    { }

    public Policy(IDomainRule[] domainRules, params IAsyncDomainRule[] asyncDomainRules)
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
