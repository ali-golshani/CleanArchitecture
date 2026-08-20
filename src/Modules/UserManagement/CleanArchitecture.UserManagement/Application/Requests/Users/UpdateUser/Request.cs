using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.Users.UpdateUser;

public sealed class Request : RequestBase, IRequest<Request, Empty>
{
    public override string RequestTitle => "Update User";

    public required Guid UserId { get; init; }

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string PhoneNumber { get; init; }
}
