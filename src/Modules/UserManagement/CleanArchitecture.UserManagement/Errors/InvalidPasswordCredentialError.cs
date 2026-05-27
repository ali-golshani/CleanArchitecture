using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidPasswordCredentialError()
    : Error(ErrorType.Forbidden, ErrorMessages.InvalidPasswordCredentialError);