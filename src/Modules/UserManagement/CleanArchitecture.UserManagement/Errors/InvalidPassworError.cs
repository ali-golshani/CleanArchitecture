using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidPassworError()
    : Error(ErrorCodes.InvalidPassword, ErrorType.Forbidden, ErrorMessages.InvalidPassworError);
