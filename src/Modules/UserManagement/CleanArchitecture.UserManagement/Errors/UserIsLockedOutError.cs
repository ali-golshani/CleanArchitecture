using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class UserIsLockedOutError()
    : Framework.Results.Errors.ForbiddenError(ErrorMessages.UserIsLockedOutError);