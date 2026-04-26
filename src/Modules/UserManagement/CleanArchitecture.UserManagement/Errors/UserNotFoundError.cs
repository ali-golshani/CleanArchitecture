using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class UserNotFoundError()
    : Framework.Results.Errors.NotFoundError(ErrorMessages.UserNotFoundError);