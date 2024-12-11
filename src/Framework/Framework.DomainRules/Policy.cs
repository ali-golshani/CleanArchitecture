namespace Framework.DomainRules;

public class Policy
{
    public static readonly Policy Empty = new Policy([], []);

    private readonly IReadOnlyCollection<IDomainRule> domainRules;
    private readonly IReadOnlyCollection<IAsyncDomainRule> asyncDomainRules;

    public static Policy operator +(Policy left, IDomainRule domainRule)
    {
        var domainRules = left.domainRules.Concat([domainRule]).ToList();
        return new Policy(domainRules, left.asyncDomainRules);
    }

    public static Policy operator +(Policy left, IAsyncDomainRule asyncDomainRule)
    {
        var asyncDomainRules = left.asyncDomainRules.Concat([asyncDomainRule]).ToList();
        return new Policy(left.domainRules, asyncDomainRules);
    }

    public static Policy operator +(Policy left, Policy right)
    {
        var domainRules = left.domainRules.Concat(right.domainRules).ToList();
        var asyncDomainRules = left.asyncDomainRules.Concat(right.asyncDomainRules).ToList();
        return new Policy(domainRules, asyncDomainRules);
    }

    public Policy(params IDomainRule[] domainRules)
        : this(domainRules, [])
    { }

    public Policy(params IAsyncDomainRule[] asyncDomainRules)
        : this([], asyncDomainRules)
    { }

    public Policy(IReadOnlyCollection<IDomainRule> domainRules)
        : this(domainRules, [])
    { }

    public Policy(IReadOnlyCollection<IAsyncDomainRule> asyncDomainRules)
        : this([], asyncDomainRules)
    { }

    public Policy(IReadOnlyCollection<IDomainRule> domainRules, IReadOnlyCollection<IAsyncDomainRule> asyncDomainRules)
    {
        this.domainRules = domainRules;
        this.asyncDomainRules = asyncDomainRules;
    }

    public async IAsyncEnumerable<Clause> Evaluate()
    {
        foreach (var rule in domainRules)
        {
            foreach (var clause in rule.Evaluate())
            {
                yield return clause;
            }
        }

        foreach (var rule in asyncDomainRules)
        {
            await foreach (var clause in rule.Evaluate())
            {
                yield return clause;
            }
        }
    }
}
