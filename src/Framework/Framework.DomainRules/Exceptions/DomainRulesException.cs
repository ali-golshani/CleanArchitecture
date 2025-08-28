using Framework.DomainRules.Extensions;

namespace Framework.DomainRules.Exceptions;

public class DomainRulesException : DomainException
{
    public DomainRulesException(IReadOnlyCollection<Clause> clauses)
        : base(clauses.Select(x => x.Statement).JoinString())
    {
        Clauses = clauses;
        Messages = [.. Clauses.Select(x => x.Statement)];
        ValidationMessages = Clauses.JoinString();
    }

    public IReadOnlyCollection<Clause> Clauses { get; }
    public override IReadOnlyCollection<string> Messages { get; }
    public string ValidationMessages { get; }

    public override UserFriendlyException ToUserFriendlyException(bool isRegistered)
    {
        return new UserFriendlyDomainRulesException(this, isRegistered);
    }
}
