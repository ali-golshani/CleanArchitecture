namespace Framework.DomainRules.Wrappers;

internal sealed class ConditionalDomainRuleWrapper(bool condition, params IDomainRule[] rules) : IDomainRule
{
    public bool Condition { get; } = condition;
    public IDomainRule[] Rules { get; } = rules;

    public IEnumerable<Error> Evaluate()
    {
        if (Condition)
        {
            return Rules.SelectMany(x => x.Evaluate());
        }
        else
        {
            return [];
        }
    }
}
