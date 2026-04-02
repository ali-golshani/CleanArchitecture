namespace CleanArchitecture.UserManagement.Errors;

public sealed class UnauthorizedError : Framework.Results.Errors.UnauthorizedError
{
    private const string ErrorMessage = "کاربر احراز هویت تشده است";

    public UnauthorizedError() : base(ErrorMessage)
    { }
}