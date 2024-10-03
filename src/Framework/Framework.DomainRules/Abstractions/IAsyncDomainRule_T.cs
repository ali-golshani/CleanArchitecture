namespace Framework.DomainRules;

public interface IAsyncDomainRule<in T>
{
    IAsyncEnumerable<Clause> Evaluate(T value);
}
