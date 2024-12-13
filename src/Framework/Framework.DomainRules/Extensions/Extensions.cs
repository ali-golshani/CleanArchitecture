using Framework.DomainRules.Wrappers;

namespace Framework.DomainRules.Extensions;

public static class Extensions
{
    public static async Task<List<Error>> Errors(this DomainPolicy policy)
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

    public static async IAsyncEnumerable<Clause> Evaluate(this IEnumerable<IAsyncDomainRule> rules)
    {
        foreach (var validator in rules)
        {
            await foreach (var clause in validator.Evaluate())
            {
                yield return clause;
            }
        }
    }

    #endregion

    #region where

    public static IDomainRule Where(this IDomainRule validator, bool condition)
    {
        return new ConditionalWrapper(condition, validator);
    }

    public static IDomainRule Where<TConditions>(this IDomainRule validator, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalWrapper(predicate(conditions), validator);
    }

    public static IDomainRule Where(this IDomainRule[] rules, bool condition)
    {
        return new ConditionalWrapper(condition, rules);
    }

    public static IDomainRule Where<TConditions>(this IDomainRule[] rules, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalWrapper(predicate(conditions), rules);
    }

    #endregion

    #region async where

    public static IAsyncDomainRule Where(this IAsyncDomainRule validator, bool condition)
    {
        return new AsyncConditionalWrapper(condition, validator);
    }

    public static IAsyncDomainRule Where<TConditions>(this IAsyncDomainRule validator, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new AsyncConditionalWrapper(predicate(conditions), validator);
    }

    public static IAsyncDomainRule Where(this IAsyncDomainRule[] rules, bool condition)
    {
        return new AsyncConditionalWrapper(condition, rules);
    }

    public static IAsyncDomainRule Where<TConditions>(this IAsyncDomainRule[] rules, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new AsyncConditionalWrapper(predicate(conditions), rules);
    }

    #endregion

    public static IDomainRule AsSync(this IAsyncDomainRule validator)
    {
        return new SyncWrapper(validator);
    }

    public static IAsyncDomainRule AsAsync(this IDomainRule validator)
    {
        return new AsyncWrapper(validator);
    }
}
