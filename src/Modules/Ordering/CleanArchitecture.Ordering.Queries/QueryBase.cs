using Framework.Mediator;

namespace CleanArchitecture.Ordering.Queries;

public abstract class QueryBase : Query
{
    private protected QueryBase() { }

    public override string LoggingDomain => nameof(Ordering);
}
