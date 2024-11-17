using Framework.DomainRules.Extensions;

namespace Framework.DomainRules.Exceptions;

public class DomainRulesException : DomainException
{
    public DomainRulesException(IReadOnlyCollection<Clause> clauses)
    {
        Clauses = clauses;
        Message = Clauses.Select(x => x.Statement).JoinString();
        Messages = Clauses.Select(x => x.Statement).ToList();
        ValidationMessages = Clauses.JoinString();
    }

    public IReadOnlyCollection<Clause> Clauses { get; }
    public override string Message { get; }
    public override IReadOnlyCollection<string> Messages { get; }
    public string ValidationMessages { get; }

    public override UserFriendlyException ToUserFriendlyException(bool isRegistered)
    {
        return new UserFriendlyDomainRulesException(this, isRegistered);
    }
}
