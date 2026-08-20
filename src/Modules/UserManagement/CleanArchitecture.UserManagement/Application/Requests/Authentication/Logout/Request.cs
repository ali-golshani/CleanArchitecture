using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.Logout;

public sealed class Request : RequestBase, IRequest<Request, Empty>
{
    public override string RequestTitle => "Logout";

    public required string RefreshToken { get; init; }
}