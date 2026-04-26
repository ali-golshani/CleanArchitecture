using CleanArchitecture.UserManagement.Domain;
using CleanArchitecture.UserManagement.Domain.Repositories;
using CleanArchitecture.UserManagement.Domain.Services;
using CleanArchitecture.UserManagement.Domain.Services.Otp;
using CleanArchitecture.UserManagement.Errors;
using CleanArchitecture.UserManagement.Utilities;
using Framework.Mediator;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Application.Requests.Authentication.RequestSmsOtpForLogin;

internal sealed class Handler(
    TokenLifetimeService lifetimeService,
    IUserRepository userRepository,
    IOneTimePasswordRepository oneTimePasswordRepository,
    SmsOtpChannel otpChannel) : IRequestHandler<Request, Response>
{
    private readonly TokenLifetimeService lifetimeService = lifetimeService;
    private readonly IUserRepository userRepository = userRepository;
    private readonly IOneTimePasswordRepository oneTimePasswordRepository = oneTimePasswordRepository;
    private readonly SmsOtpChannel otpChannel = otpChannel;

    public async Task<Result<Response>> Handle(Request request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByPhoneNumber(request.MobileNumber);

        if (user is null)
        {
            return new MobileNumberNotFoundError();
        }

        if (!user.IsActive)
        {
            return new UserIsLockedOutError();
        }

        var otpCode = OtpBuilder.BuildOtp(Settings.OtpLength);
        var otpExpirationTime = lifetimeService.SmsOtpExpirationTime(Shared.SystemClock.Now);
        var otp = new OneTimePassword(OneTimePasswordPurpose.Login, user.Id, otpCode, otpExpirationTime);
        oneTimePasswordRepository.Add(otp);

        var otpTarget = new SmsOtpTarget(request.MobileNumber);
        await otpChannel.SendAsync(otpCode, otpTarget, cancellationToken);

        return new Response
        {
            OtpId = otp.Id
        };
    }
}