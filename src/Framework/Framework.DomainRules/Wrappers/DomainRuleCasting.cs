using Framework.DomainRules.Extensions;

namespace Framework.DomainRules;

internal static class DomainRuleCasting<T>
{
    internal sealed class NonGenericDomainRule(T value) : IDomainRule
    {
        public T Value { get; } = value;
        public IDomainRule<T>[] Validators { get; init; } = [];

        public IEnumerable<Clause> Evaluate()
        {
            return Validators.Evaluate(Value);
        }
    }

    internal sealed class InheritedDomainRule<TInherited> : IDomainRule<T>
        where TInherited : T
    {
        public IDomainRule<TInherited>[] Validators { get; init; } = [];

        public IEnumerable<Clause> Evaluate(T value)
        {
            if (value is TInherited inheritedValue)
            {
                return Validators.Evaluate(inheritedValue);
            }
            else
            {
                return Array.Empty<Clause>();
            }
        }
    }
}
