using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class UsernameAlreadyExistsError()
    : Error(ErrorCodes.UsernameAlreadyExists, ErrorType.Forbidden, ErrorMessages.UsernameAlreadyExistsError);
