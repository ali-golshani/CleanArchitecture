namespace Framework.DomainRules;

public interface IBusinessRule
{
    IAsyncEnumerable<Error> Evaluate();
}
