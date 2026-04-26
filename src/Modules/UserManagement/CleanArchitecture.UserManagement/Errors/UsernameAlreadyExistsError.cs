using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class UsernameAlreadyExistsError()
    : Framework.Results.Errors.ForbiddenError(ErrorMessages.UsernameAlreadyExistsError);