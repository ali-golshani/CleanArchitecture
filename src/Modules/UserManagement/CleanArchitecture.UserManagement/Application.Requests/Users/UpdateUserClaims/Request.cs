using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUserClaims;

public sealed class Request : RequestBase, IRequest<Request, Empty>
{
    public override string RequestTitle => "Update User Claims";

    public required Guid UserId { get; init; }

    public required IReadOnlyCollection<Models.Claim> Claims { get; init; }
}
