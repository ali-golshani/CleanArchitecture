namespace CleanArchitecture.UserManagement;

public static class ErrorCodes
{
    public const string ConsumedOtp = "user.otp.consumed";
    public const string ExpiredOtp = "user.otp.expired";
    public const string ExpiredSession = "user.session.expired";
    public const string InvalidOtpCredential = "user.otp.credential.invalid";
    public const string InvalidOtp = "user.otp.invalid";
    public const string InvalidPasswordCredential = "user.password.credential.invalid";
    public const string InvalidPassword = "user.password.invalid";
    public const string MobileNumberNotFound = "user.mobile_number.not_found";
    public const string Unauthorized = "user.unauthorized";
    public const string LockedOut = "user.locked_out";
    public const string UsernameAlreadyExists = "user.username.already_exists";
    public const string NotFound = "user.not_found";
}
