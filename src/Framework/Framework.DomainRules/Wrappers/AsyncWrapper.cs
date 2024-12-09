namespace Framework.DomainRules;

internal sealed class AsyncWrapper(IDomainRule rule) : IAsyncDomainRule
{
    public async IAsyncEnumerable<Clause> Evaluate()
    {
        await Task.CompletedTask;

        foreach (var clause in rule.Evaluate())
        {
            yield return clause;
        }
    }
}
