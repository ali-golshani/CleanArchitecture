using Framework.DomainRules.Wrappers;

namespace Framework.DomainRules.Extensions;

public static class WhereExtensions
{
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
}
