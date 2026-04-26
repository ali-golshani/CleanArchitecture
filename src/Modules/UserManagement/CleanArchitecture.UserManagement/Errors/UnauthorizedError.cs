using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class UnauthorizedError()
    : Framework.Results.Errors.UnauthorizedError(ErrorMessages.UnauthorizedError);