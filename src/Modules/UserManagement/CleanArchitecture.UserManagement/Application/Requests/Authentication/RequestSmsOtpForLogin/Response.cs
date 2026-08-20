using Framework.Mediator;

namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.RequestSmsOtpForLogin;

public sealed class Response : IResponse
{
    public required Guid OtpId { get; init; }
}