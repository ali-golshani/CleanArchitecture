using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidPassworError()
    : Framework.Results.Errors.ForbiddenError(ErrorMessages.InvalidPassworError);