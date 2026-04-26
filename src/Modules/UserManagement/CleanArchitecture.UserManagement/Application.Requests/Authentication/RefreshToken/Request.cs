namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.RefreshToken;

public sealed class Request : RequestBase, IRequest<Request, Response>
{
    public override string RequestTitle => "Refresh Token";

    public required string RefreshToken { get; init; }
}