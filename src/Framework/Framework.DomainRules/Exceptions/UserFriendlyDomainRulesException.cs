namespace Framework.DomainRules;

public class UserFriendlyDomainRulesException : UserFriendlyException
{
    internal UserFriendlyDomainRulesException(DomainRulesException exception, bool isRegistered)
        : base(exception.Message, exception.CorrelationId, isRegistered)
    {
        Clauses = exception.Clauses.Select(x => x.Statement).ToList();
    }

    public IReadOnlyCollection<string> Clauses { get; }
}
