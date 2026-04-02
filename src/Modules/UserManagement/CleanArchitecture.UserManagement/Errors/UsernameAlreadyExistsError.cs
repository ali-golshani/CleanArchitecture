namespace CleanArchitecture.UserManagement.Errors;

public sealed class UsernameAlreadyExistsError : Framework.Results.Errors.ForbiddenError
{
    private const string ErrorMessage = "نام کاربری در سامانه موجود است";

    public UsernameAlreadyExistsError() : base(ErrorMessage) { }
}
