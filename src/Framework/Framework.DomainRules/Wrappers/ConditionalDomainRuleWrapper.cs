using Framework.DomainRules.Extensions;

namespace Framework.DomainRules.Wrappers;

internal sealed class ConditionalDomainRuleWrapper(bool condition, params IDomainRule[] rules) : IDomainRule
{
    public bool Condition { get; } = condition;
    public IDomainRule[] Rules { get; } = rules;

    public IEnumerable<Clause> Evaluate()
    {
        if (Condition)
        {
            return Rules.Evaluate();
        }
        else
        {
            return [];
        }
    }
}
