using Framework.DomainRules.Extensions;

namespace Framework.DomainRules;

internal sealed class ConditionalWrapper<T>(Func<T, bool> condition, params IDomainRule<T>[] validators)
    : IDomainRule<T>
{
    public Func<T, bool> Condition { get; } = condition;
    public IDomainRule<T>[] Validators { get; } = validators;

    public IEnumerable<Clause> Evaluate(T value)
    {
        if (Condition(value))
        {
            return Validators.Evaluate(value);
        }
        else
        {
            return [];
        }
    }
}
