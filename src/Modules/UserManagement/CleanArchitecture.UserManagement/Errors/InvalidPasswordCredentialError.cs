using CleanArchitecture.UserManagement.Resources;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidPasswordCredentialError()
    : Framework.Results.Errors.ForbiddenError(ErrorMessages.InvalidPasswordCredentialError);