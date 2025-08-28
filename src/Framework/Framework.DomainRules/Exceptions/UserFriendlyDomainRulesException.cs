namespace Framework.DomainRules.Exceptions;

public class UserFriendlyDomainRulesException : UserFriendlyException
{
    internal UserFriendlyDomainRulesException(DomainRulesException exception, bool isRegistered)
        : base(exception.Message, exception.TraceId, isRegistered)
    {
        Messages = [.. exception.Clauses.Select(x => x.Statement)];
    }

    public override IReadOnlyCollection<string> Messages { get; }
}
