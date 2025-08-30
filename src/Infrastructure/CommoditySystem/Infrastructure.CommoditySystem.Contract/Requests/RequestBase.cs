using Framework.Mediator;

namespace Infrastructure.CommoditySystem.Requests;

public abstract class RequestBase : Request
{
    private protected RequestBase() { }

    public override bool? ShouldLog => false;
    public override string LoggingDomain => nameof(CommoditySystem);
}
