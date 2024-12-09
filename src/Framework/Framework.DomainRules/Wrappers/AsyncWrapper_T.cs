namespace Framework.DomainRules;

internal sealed class AsyncWrapper<T>(IDomainRule<T> validator) : IAsyncDomainRule<T>
{
    public async IAsyncEnumerable<Clause> Evaluate(T value)
    {
        await Task.CompletedTask;

        foreach (var clause in validator.Evaluate(value))
        {
            yield return clause;
        }
    }
}
