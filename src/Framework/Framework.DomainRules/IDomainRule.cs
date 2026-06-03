namespace Framework.DomainRules;

public interface IDomainRule
{
    IEnumerable<Error> Evaluate();
}
