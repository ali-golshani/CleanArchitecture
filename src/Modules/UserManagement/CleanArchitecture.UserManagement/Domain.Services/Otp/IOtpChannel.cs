namespace CleanArchitecture.UserManagement.Domain.Services.Otp;

internal interface IOtpChannel<in TTarget>
    where TTarget : OtpTarget
{
    Task SendAsync(string otp, TTarget target, CancellationToken cancellationToken);
}

