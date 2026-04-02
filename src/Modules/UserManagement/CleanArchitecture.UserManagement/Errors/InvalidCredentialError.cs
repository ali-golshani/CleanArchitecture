namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidCredentialError : Framework.Results.Errors.ForbiddenError
{
    private const string ErrorMessage = "نام کاربری و رمز عبور اشتباه است";

    public InvalidCredentialError() : base(ErrorMessage) { }
}