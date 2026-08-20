using Framework.Mediator;

namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.LoginByOtp;

public sealed class Response : IResponse
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
    public required int ExpiresIn { get; init; }
}