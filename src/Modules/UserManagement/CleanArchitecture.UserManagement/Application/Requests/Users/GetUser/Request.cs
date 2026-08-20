using CleanArchitecture.UserManagement.Application.Models;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.GetUser;

public sealed class Request : RequestBase, IRequest<Request, User?>
{
    public override bool? ShouldLog => false;
    public override string RequestTitle => "Get User";

    public required Guid UserId { get; init; }
}
