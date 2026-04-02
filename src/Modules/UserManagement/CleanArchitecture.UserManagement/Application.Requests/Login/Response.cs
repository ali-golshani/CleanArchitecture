using Framework.Mediator;

namespace CleanArchitecture.UserManagement.Application.Requests.Login;

public sealed class Response : IResponse
{
    public required string Token { get; init; }
    public required string RefreshToken { get; init; }
}