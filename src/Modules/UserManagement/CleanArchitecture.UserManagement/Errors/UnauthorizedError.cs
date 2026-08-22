using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class UnauthorizedError()
    : Error(ErrorCodes.Unauthorized, ErrorType.Unauthorized, ErrorMessages.UnauthorizedError);
