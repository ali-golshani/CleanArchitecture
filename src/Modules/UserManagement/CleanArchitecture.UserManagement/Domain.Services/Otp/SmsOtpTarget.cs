namespace CleanArchitecture.UserManagement.Domain.Services.Otp;

internal sealed record SmsOtpTarget(string MobileNumber) : OtpTarget;