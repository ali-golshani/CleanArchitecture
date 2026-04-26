namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.RequestSmsOtpForLogin;

public sealed class Request : RequestBase, IRequest<Request, Response>
{
    public override string RequestTitle => "Request Sms Otp for Login";

    public required string MobileNumber { get; init; }
}