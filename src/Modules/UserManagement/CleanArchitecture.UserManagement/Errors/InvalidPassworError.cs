using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class InvalidPassworError()
    : Error(ErrorType.Forbidden, ErrorMessages.InvalidPassworError);