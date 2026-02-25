using Framework.DomainRules.Wrappers;

namespace Framework.DomainRules.Extensions;

public static class Extensions
{
    public static async Task<List<Error>> EvaluateAndReturnErrors(this BusinessPolicy policy)
    {
        var result = new List<Error>();
        await foreach (var item in policy.Evaluate().Errors())
        {
            result.Add(item);
        }
        return result;
    }

    #region array validate

    public static IEnumerable<Clause> Evaluate(this IEnumerable<IDomainRule> rules)
    {
        return rules.SelectMany(x => x.Evaluate());
    }

    public static async IAsyncEnumerable<Clause> Evaluate(this IEnumerable<IBusinessRule> rules)
    {
        foreach (var rule in rules)
        {
            await foreach (var clause in rule.Evaluate())
            {
                yield return clause;
            }
        }
    }

    #endregion

    #region where

    public static IDomainRule Where(this IDomainRule rule, bool condition)
    {
        return new ConditionalDomainRuleWrapper(condition, rule);
    }

    public static IDomainRule Where<TConditions>(this IDomainRule rule, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalDomainRuleWrapper(predicate(conditions), rule);
    }

    public static IDomainRule Where(this IDomainRule[] rules, bool condition)
    {
        return new ConditionalDomainRuleWrapper(condition, rules);
    }

    public static IDomainRule Where<TConditions>(this IDomainRule[] rules, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalDomainRuleWrapper(predicate(conditions), rules);
    }

    #endregion

    #region async where

    public static IBusinessRule Where(this IBusinessRule rule, bool condition)
    {
        return new ConditionalBusinessRuleWrapper(condition, rule);
    }

    public static IBusinessRule Where<TConditions>(this IBusinessRule rule, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalBusinessRuleWrapper(predicate(conditions), rule);
    }

    public static IBusinessRule Where(this IBusinessRule[] rules, bool condition)
    {
        return new ConditionalBusinessRuleWrapper(condition, rules);
    }

    public static IBusinessRule Where<TConditions>(this IBusinessRule[] rules, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalBusinessRuleWrapper(predicate(conditions), rules);
    }

    #endregion
}
