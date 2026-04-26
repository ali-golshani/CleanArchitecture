using CleanArchitecture.UserManagement.Contracts;

namespace CleanArchitecture.UserManagement.Domain.Services.Otp;

internal sealed class SmsOtpChannel(
    ISmsSender smsSender,
    ISmsTextBuilder smsTextBuilder) : IOtpChannel<SmsOtpTarget>
{
    private readonly ISmsSender smsSender = smsSender;
    private readonly ISmsTextBuilder smsTextBuilder = smsTextBuilder;

    public async Task SendAsync(string otp, SmsOtpTarget target, CancellationToken cancellationToken)
    {
        var text = smsTextBuilder.BuildOtpText(otp);
        await smsSender.SendMessageAsync(text, target.MobileNumber, cancellationToken);
    }
}