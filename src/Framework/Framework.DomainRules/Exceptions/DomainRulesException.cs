namespace Framework.DomainRules;

public class DomainRulesException(IEnumerable<Clause> clauses) : DomainException
{
    public IEnumerable<Clause> Clauses { get; } = clauses;

    public override string Message => Clauses.Select(x => x.Statement).JoinString();
    public string ValidationMessages => Clauses.JoinString();

    public override UserFriendlyException ToUserFriendlyException(bool isRegistered)
    {
        return new UserFriendlyDomainRulesException(this, isRegistered);
    }
}
