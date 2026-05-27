using CleanArchitecture.UserManagement.Resources;
using Framework.Results;

namespace CleanArchitecture.UserManagement.Errors;

public sealed class ExpiredSessionError()
    : Error(ErrorType.Forbidden, ErrorMessages.ExpiredSessionError);