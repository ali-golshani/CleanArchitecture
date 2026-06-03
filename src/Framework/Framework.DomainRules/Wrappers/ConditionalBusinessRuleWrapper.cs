namespace Framework.DomainRules.Wrappers;

internal sealed class ConditionalBusinessRuleWrapper(bool condition, params IBusinessRule[] rules) : IBusinessRule
{
    public bool Condition { get; } = condition;
    public IBusinessRule[] Rules { get; } = rules;

    public async IAsyncEnumerable<Error> Evaluate()
    {
        if (Condition)
        {
            foreach (var rule in Rules)
            {
                await foreach (var error in rule.Evaluate())
                {
                    yield return error;
                }
            }
        }
    }
}
