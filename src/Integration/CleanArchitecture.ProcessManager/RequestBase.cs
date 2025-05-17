using Framework.Mediator;

namespace CleanArchitecture.ProcessManager;

public abstract class RequestBase : Request
{
    private protected RequestBase() { }

    public override string LggingDomain => nameof(ProcessManager);
}
