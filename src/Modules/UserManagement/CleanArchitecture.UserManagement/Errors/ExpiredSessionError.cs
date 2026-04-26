using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class ExpiredSessionError()
    : Framework.Results.Errors.ForbiddenError(ErrorMessages.ExpiredSessionError);