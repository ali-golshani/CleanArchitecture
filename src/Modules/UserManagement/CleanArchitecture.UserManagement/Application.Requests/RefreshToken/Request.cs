namespace CleanArchitecture.UserManagement.Application.Requests.RefreshToken;

public sealed class Request : RequestBase, IRequest<Request, Response>
{
    public override string RequestTitle => "Refresh Token";

    public required string Token { get; init; }
    public required string RefreshToken { get; init; }
}