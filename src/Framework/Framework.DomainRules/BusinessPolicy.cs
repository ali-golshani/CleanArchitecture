namespace Framework.DomainRules;

public sealed class BusinessPolicy(IDomainRule[] domainRules, params IBusinessRule[] businessRules)
{
    public IDomainRule[] DomainRules { get; } = domainRules;
    public IBusinessRule[] BusinessRules { get; } = businessRules;

    public BusinessPolicy(params IDomainRule[] domainRules)
        : this(domainRules, [])
    { }

    public BusinessPolicy(params IBusinessRule[] businessRules)
        : this([], businessRules)
    { }

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
