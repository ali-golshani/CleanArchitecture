namespace Framework.DomainRules;

public sealed class BusinessPolicy
{
    public IDomainRule[] DomainRules { get; }
    public IBusinessRule[] BusinessRules { get; }

    public BusinessPolicy(params IDomainRule[] domainRules)
        : this(domainRules, [])
    { }

    public BusinessPolicy(params IBusinessRule[] businessRules)
        : this([], businessRules)
    { }

    public BusinessPolicy(IDomainRule[] domainRules, params IBusinessRule[] businessRules)
    {
        DomainRules = domainRules;
        BusinessRules = businessRules;
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

        foreach (var rule in BusinessRules)
        {
            await foreach (var clause in rule.Evaluate())
            {
                yield return clause;
            }
        }
    }
}
