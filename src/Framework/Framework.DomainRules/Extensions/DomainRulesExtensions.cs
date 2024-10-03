namespace Framework.DomainRules;

public static class DomainRulesExtensions
{
    #region results

    public static IEnumerable<Error> Errors(this IEnumerable<Clause> clauses)
    {
        return
            clauses
            .Where(x => x.IsFalse)
            .Select(ToError);
    }

    public static async IAsyncEnumerable<Error> Errors(this IAsyncEnumerable<Clause> clauses)
    {
        await foreach (var clause in clauses)
        {
            if (clause.IsFalse)
            {
                yield return ToError(clause);
            }
        }
    }

    private static Error ToError(Clause clause)
    {
        return new Error
        (
            type: ErrorType.Validation,
            message: clause.Statement,
            sources: clause.Sources.Select(ToErrorSource).ToArray()
        );
    }

    private static ErrorSource ToErrorSource(ClauseSource source)
    {
        return new ErrorSource(source.Name, source.Value);
    }

    #endregion

    #region throw

    public static void Throw(this IEnumerable<Clause> clauses)
    {
        var brokenRules = clauses.Where(x => !x.IsTrue).ToList();

        if (brokenRules.Count > 0)
        {
            throw new DomainRulesException(brokenRules);
        }
    }

    public static async Task Throw(this IAsyncEnumerable<Clause> clauses)
    {
        var brokenRules = new List<Clause>();

        await foreach (var clause in clauses)
        {
            if (!clause.IsTrue)
            {
                brokenRules.Add(clause);
            }
        }

        if (brokenRules.Count > 0)
        {
            throw new DomainRulesException(brokenRules);
        }
    }

    #endregion

    #region array validate

    public static IEnumerable<Clause> Evaluate(this IEnumerable<IDomainRule> rules)
    {
        return rules.SelectMany(x => x.Evaluate());
    }

    public static IEnumerable<Clause> Evaluate<T>(this IEnumerable<IDomainRule<T>> rules, T value)
    {
        return rules.SelectMany(x => x.Evaluate(value));
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

    public static async IAsyncEnumerable<Clause> Evaluate<T>(this IEnumerable<IAsyncDomainRule<T>> rules, T value)
    {
        foreach (var validator in rules)
        {
            await foreach (var clause in validator.Evaluate(value))
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

    public static IDomainRule<T> Where<T>(this IDomainRule<T> validator, bool condition)
    {
        return new ConditionalWrapper<T>(_ => condition, validator);
    }

    public static IDomainRule Where<TConditions>(this IDomainRule validator, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalWrapper(predicate(conditions), validator);
    }

    public static IDomainRule<TValue> Where<TValue, TConditions>(this IDomainRule<TValue> validator, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalWrapper<TValue>(_ => predicate(conditions), validator);
    }

    public static IDomainRule<TValue> Where<TValue>(this IDomainRule<TValue> validator, Func<TValue, bool> predicate)
    {
        return new ConditionalWrapper<TValue>(predicate, validator);
    }

    public static IDomainRule Where(this IDomainRule[] rules, bool condition)
    {
        return new ConditionalWrapper(condition, rules);
    }

    public static IDomainRule<T> Where<T>(this IDomainRule<T>[] rules, bool condition)
    {
        return new ConditionalWrapper<T>(_ => condition, rules);
    }

    public static IDomainRule Where<TConditions>(this IDomainRule[] rules, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalWrapper(predicate(conditions), rules);
    }

    public static IDomainRule<TValue> Where<TValue, TConditions>(this IDomainRule<TValue>[] rules, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new ConditionalWrapper<TValue>(_ => predicate(conditions), rules);
    }

    public static IDomainRule<TValue> Where<TValue>(this IDomainRule<TValue>[] rules, Func<TValue, bool> predicate)
    {
        return new ConditionalWrapper<TValue>(predicate, rules);
    }

    #endregion

    #region async where

    public static IAsyncDomainRule Where(this IAsyncDomainRule validator, bool condition)
    {
        return new AsyncConditionalWrapper(condition, validator);
    }

    public static IAsyncDomainRule<T> Where<T>(this IAsyncDomainRule<T> validator, bool condition)
    {
        return new AsyncConditionalWrapper<T>(_ => condition, validator);
    }

    public static IAsyncDomainRule Where<TConditions>(this IAsyncDomainRule validator, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new AsyncConditionalWrapper(predicate(conditions), validator);
    }

    public static IAsyncDomainRule<TValue> Where<TValue, TConditions>(this IAsyncDomainRule<TValue> validator, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new AsyncConditionalWrapper<TValue>(_ => predicate(conditions), validator);
    }

    public static IAsyncDomainRule<TValue> Where<TValue>(this IAsyncDomainRule<TValue> validator, Func<TValue, bool> predicate)
    {
        return new AsyncConditionalWrapper<TValue>(predicate, validator);
    }

    public static IAsyncDomainRule Where(this IAsyncDomainRule[] rules, bool condition)
    {
        return new AsyncConditionalWrapper(condition, rules);
    }

    public static IAsyncDomainRule<T> Where<T>(this IAsyncDomainRule<T>[] rules, bool condition)
    {
        return new AsyncConditionalWrapper<T>(_ => condition, rules);
    }

    public static IAsyncDomainRule Where<TConditions>(this IAsyncDomainRule[] rules, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new AsyncConditionalWrapper(predicate(conditions), rules);
    }

    public static IAsyncDomainRule<TValue> Where<TValue, TConditions>(this IAsyncDomainRule<TValue>[] rules, TConditions conditions, Func<TConditions, bool> predicate)
    {
        return new AsyncConditionalWrapper<TValue>(_ => predicate(conditions), rules);
    }

    public static IAsyncDomainRule<TValue> Where<TValue>(this IAsyncDomainRule<TValue>[] rules, Func<TValue, bool> predicate)
    {
        return new AsyncConditionalWrapper<TValue>(predicate, rules);
    }

    #endregion

    public static IDomainRule AsSync(this IAsyncDomainRule validator)
    {
        return new SyncWrapper(validator);
    }

    public static IDomainRule<T> AsSync<T>(this IAsyncDomainRule<T> validator)
    {
        return new SyncWrapper<T>(validator);
    }

    public static IDomainRule AsNonGeneric<T>(this IDomainRule<T> validator, T value)
    {
        return new DomainRuleCasting<T>.NonGenericDomainRule(value)
        {
            Validators = new IDomainRule<T>[]
            {
                validator
            }
        };
    }

    public static IDomainRule AsNonGeneric<T>(this IDomainRule<T>[] rules, T value)
    {
        return new DomainRuleCasting<T>.NonGenericDomainRule(value)
        {
            Validators = rules
        };
    }

    public static IDomainRule<T> AsInheritedValidator<T, TInherited>(this IDomainRule<TInherited> validator)
        where TInherited : T
    {
        return new DomainRuleCasting<T>.InheritedDomainRule<TInherited>
        {
            Validators = new IDomainRule<TInherited>[]
            {
                validator
            }
        };
    }

    public static IDomainRule<T> AsInheritedValidator<T, TInherited>(this IDomainRule<TInherited>[] rules)
        where TInherited : T
    {
        return new DomainRuleCasting<T>.InheritedDomainRule<TInherited>
        {
            Validators = rules
        };
    }
}
