namespace CleanArchitecture.UserManagement.Application.Requests.Users.GetUsers;

public sealed class Request : RequestBase, IRequest<Request, IReadOnlyCollection<Models.User>>
{
    public override bool? ShouldLog => false;
    public override string RequestTitle => "Get Users";

    public string? Name { get; init; }
    public string? PhoneNumber { get; init; }
}
