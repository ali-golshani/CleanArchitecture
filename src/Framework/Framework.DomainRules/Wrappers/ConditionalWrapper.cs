using Framework.DomainRules.Extensions;

namespace Framework.DomainRules;

internal sealed class ConditionalWrapper(bool condition, params IDomainRule[] validators) : IDomainRule
{
    public bool Condition { get; } = condition;
    public IDomainRule[] Validators { get; } = validators;

    public IEnumerable<Clause> Evaluate()
    {
        if (Condition)
        {
            return Validators.Evaluate();
        }
        else
        {
            return [];
        }
    }
}
