using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.ResetPassword;

public sealed class Request : RequestBase, IRequest<Request, Empty>
{
    public override string RequestTitle => "Reset Password";

    public required Guid UserId { get; init; }
}
