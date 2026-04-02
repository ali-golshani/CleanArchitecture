namespace CleanArchitecture.UserManagement.Errors;

public sealed class UserNotFoundError : Framework.Results.Errors.NotFoundError
{
    private const string ErrorMessage = "شناسه کاربر نامعتبر است";

    public UserNotFoundError() : base(ErrorMessage) { }
}