using CleanArchitecture.UserManagement.Contracts;

namespace CleanArchitecture.UserManagement.Domain.Services.Otp;

internal sealed class EmailOtpChannel(
    IEmailSender emailSender,
    IEmailContentBuilder emailContentBuilder) : IOtpChannel<EmailOtpTarget>
{
    private readonly IEmailSender emailSender = emailSender;
    private readonly IEmailContentBuilder emailContentBuilder = emailContentBuilder;

    public async Task SendAsync(string otp, EmailOtpTarget target, CancellationToken cancellationToken)
    {
        var text = emailContentBuilder.BuildOtpContent(otp);
        await emailSender.SendMailAsync(text, target.EmailAddress, cancellationToken);
    }
}