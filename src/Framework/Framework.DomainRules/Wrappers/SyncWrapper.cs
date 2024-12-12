using Framework.Exceptions.Extensions;

namespace Framework.DomainRules.Wrappers;

internal sealed class SyncWrapper(IAsyncDomainRule rule) : IDomainRule
{
    public IEnumerable<Clause> Evaluate()
    {
        return Validating().ResultWithTranslateException();
    }

    public async Task<IReadOnlyCollection<Clause>> Validating()
    {
        var result = new List<Clause>();

        await foreach (var item in rule.Evaluate())
        {
            result.Add(item);
        }

        return result;
    }
}
