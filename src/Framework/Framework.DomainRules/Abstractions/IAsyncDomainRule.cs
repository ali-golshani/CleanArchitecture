namespace Framework.DomainRules;

public interface IAsyncDomainRule
{
    IAsyncEnumerable<Clause> Evaluate();
}
