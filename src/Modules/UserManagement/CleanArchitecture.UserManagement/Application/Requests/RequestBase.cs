using Framework.Mediator;

namespace CleanArchitecture.UserManagement.Application.Requests;

public abstract class RequestBase : Request
{
    private protected RequestBase() { }

    public override bool? ShouldLog => true;
    public override string LoggingDomain => nameof(UserManagement);
}
