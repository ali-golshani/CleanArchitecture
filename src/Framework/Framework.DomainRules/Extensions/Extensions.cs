namespace Framework.DomainRules.Extensions;

public static class Extensions
{
    public static IEnumerable<Error> Evaluate(this IDomainRule[] rules)
    {
        return rules.SelectMany(x => x.Evaluate());
    }
}
