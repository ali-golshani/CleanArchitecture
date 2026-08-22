using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class UserNotFoundError()
    : Error(ErrorCodes.NotFound, ErrorType.NotFound, ErrorMessages.UserNotFoundError);
