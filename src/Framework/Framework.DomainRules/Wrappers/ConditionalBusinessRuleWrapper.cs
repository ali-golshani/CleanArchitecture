using Framework.DomainRules.Extensions;

namespace Framework.DomainRules.Wrappers;

internal sealed class ConditionalBusinessRuleWrapper(bool condition, params IBusinessRule[] rules)
    : IBusinessRule
{
    public bool Condition { get; } = condition;
    public IBusinessRule[] Rules { get; } = rules;

    public async IAsyncEnumerable<Clause> Evaluate()
    {
        if (Condition)
        {
            await foreach (var item in Rules.Evaluate())
            {
                yield return item;
            }
        }
    }
}
