namespace CleanArchitecture.UserManagement.Errors;

public sealed class UserIsLockedOutError : Framework.Results.Errors.ForbiddenError
{
    private const string ErrorMessage = "حساب کاربری شما غیر فعال است";

    public UserIsLockedOutError() : base(ErrorMessage) { }
}