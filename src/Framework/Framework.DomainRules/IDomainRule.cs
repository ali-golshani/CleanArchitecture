namespace Framework.DomainRules;

public interface IDomainRule
{
    IEnumerable<Clause> Evaluate();
}
