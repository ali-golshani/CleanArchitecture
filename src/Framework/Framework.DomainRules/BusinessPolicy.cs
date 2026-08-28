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

    public async IAsyncEnumerable<Error> Evaluate(
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var shouldBreak = false;

        foreach (var rule in DomainRules)
        {
            foreach (var error in rule.Evaluate())
            {
                shouldBreak = true;
                yield return error;
            }
        }

        if (shouldBreak)
        {
            yield break;
        }

        foreach (var rule in BusinessRules)
        {
            await foreach (var error in rule.Evaluate(cancellationToken).WithCancellation(cancellationToken))
            {
                yield return error;
                shouldBreak = true;
            }

            if (shouldBreak)
            {
                yield break;
            }
        }
    }
}
