namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.LoginByOtp;

public sealed class Request : RequestBase, IRequest<Request, Response>
{
    public override string RequestTitle => "Login by Otp";

    public required Guid OtpId { get; init; }
    public required string OtpCode { get; init; }
}