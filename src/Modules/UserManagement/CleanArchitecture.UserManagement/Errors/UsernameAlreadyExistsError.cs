using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class UsernameAlreadyExistsError()
    : Error(ErrorType.Forbidden, ErrorMessages.UsernameAlreadyExistsError);