namespace Framework.DomainRules;

public interface IBusinessRule
{
    IAsyncEnumerable<Clause> Evaluate();
}
