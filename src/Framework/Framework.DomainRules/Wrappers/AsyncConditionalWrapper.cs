using Framework.DomainRules.Extensions;

namespace Framework.DomainRules;

internal sealed class AsyncConditionalWrapper(bool condition, params IAsyncDomainRule[] validators)
    : IAsyncDomainRule
{
    public bool Condition { get; } = condition;
    public IAsyncDomainRule[] Validators { get; } = validators;

    public async IAsyncEnumerable<Clause> Evaluate()
    {
        if (Condition)
        {
            await foreach (var item in Validators.Evaluate())
            {
                yield return item;
            }
        }
    }
}
