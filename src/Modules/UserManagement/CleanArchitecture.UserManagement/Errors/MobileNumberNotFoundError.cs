using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class MobileNumberNotFoundError()
    : Framework.Results.Errors.NotFoundError(ErrorMessages.MobileNumberNotFoundError);