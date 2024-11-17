namespace Framework.DomainRules;

public interface IDomainRule<in T>
{
    IEnumerable<Clause> Evaluate(T value);
}
