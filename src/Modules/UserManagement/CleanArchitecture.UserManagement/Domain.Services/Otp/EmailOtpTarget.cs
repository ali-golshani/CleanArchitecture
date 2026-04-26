namespace CleanArchitecture.UserManagement.Domain.Services.Otp;

internal sealed record EmailOtpTarget(string EmailAddress) : OtpTarget;