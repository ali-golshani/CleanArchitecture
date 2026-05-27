using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class UserIsLockedOutError()
    : Error(ErrorType.Forbidden, ErrorMessages.UserIsLockedOutError);