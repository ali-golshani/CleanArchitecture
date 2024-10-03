namespace Framework.DomainRules;

internal sealed class SyncWrapper<T>(IAsyncDomainRule<T> validator) : IDomainRule<T>
{
    public IEnumerable<Clause> Evaluate(T value)
    {
        return Validating(value).ResultWithTranslateException();
    }

    public async Task<IReadOnlyCollection<Clause>> Validating(T value)
    {
        var result = new List<Clause>();

        await foreach (var item in validator.Evaluate(value))
        {
            result.Add(item);
        }

        return result;
    }
}
