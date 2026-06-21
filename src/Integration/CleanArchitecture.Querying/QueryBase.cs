using Framework.Mediator;

namespace CleanArchitecture.Querying;

public abstract class QueryBase : Query
{
    private protected QueryBase() { }

    public override string LoggingDomain => nameof(Querying);
}
