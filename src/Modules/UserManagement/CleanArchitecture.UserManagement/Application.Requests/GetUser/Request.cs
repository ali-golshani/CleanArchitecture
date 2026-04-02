namespace CleanArchitecture.UserManagement.Application.Requests.GetUser;

public sealed class Request : RequestBase, IRequest<Request, Models.User?>
{
    public override bool? ShouldLog => false;
    public override string RequestTitle => "Get User";

    public required Guid UserId { get; init; }
}
