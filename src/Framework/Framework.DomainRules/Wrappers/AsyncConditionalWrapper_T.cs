using Framework.DomainRules.Extensions;

namespace Framework.DomainRules;

internal sealed class AsyncConditionalWrapper<T>(Func<T, bool> condition, params IAsyncDomainRule<T>[] validators)
    : IAsyncDomainRule<T>
{
    public Func<T, bool> Condition { get; } = condition;
    public IAsyncDomainRule<T>[] Validators { get; } = validators;

    public async IAsyncEnumerable<Clause> Evaluate(T value)
    {
        if (Condition(value))
        {
            await foreach (var item in Validators.Evaluate(value))
            {
                yield return item;
            }
        }
    }
}
